using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Other
{
    public class Timer
    {
        public float RemainingTime => _remainingTime;
        public float AppointedTime => _appointedTime;
        
        public Action InterruptedIncompleted;
        public Action Ended;

        public Action<float> UpdatedEverySeconds;
        public Action<float> Updated;
        public Action<float> Started;
        public bool IsPause { get; private set; }

        private readonly CancellationToken _tokenOnDestroy;
        private CancellationTokenSource _tokenSource;
        private bool _isCountDown;
        private float _remainingTime, _appointedTime;
        private bool _isUpdateInThisSecond;

        public Timer(CancellationToken tokenOnDestroy)
        {
            Subscribe();
            _tokenOnDestroy = tokenOnDestroy;
            _tokenSource = new CancellationTokenSource();
        }

        private void Subscribe()
        {
            Ended += OnEnd;
            InterruptedIncompleted += OnEnd;
        }

        private void OnEnd()
        {
            _isUpdateInThisSecond = false;
            IsPause = false;
        }
        

        public void Set(float time) =>
            _appointedTime = time;

        public void Add(float time) =>
            _appointedTime += time;

        public void IncreaseRemainingTime(float addTime) =>
            _remainingTime += addTime;

        public void StartCountingTime(float time, bool isScaled = false)
        {
            if (time <= 0)
                return;
            
            if (IsPause)
            {
                Set(time);
                SetPauseState(false);
                return;
            }
            
            if (_isCountDown)
                return;

            Set(time);

            StartCountingTime(isScaled);
        }

        public void StartCountingTime(bool isScaled = true)
        {
            if (_appointedTime <= 0)
                return;
            
            if (IsPause)
            {
                SetPauseState(false);
                return;
            }
            
            if (_isCountDown)
                return;
            
            CountDownAsync(isScaled, _tokenSource.Token, _tokenOnDestroy).Forget();
        }

        public void Pause() => SetPauseState(true);

        public bool IsCounting() => _isCountDown;

        public void StopCountingTime()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();

            InterruptedIncompleted?.Invoke();
            _isCountDown = false;
        }

        private void SetPauseState(bool isPause)
        {
            if (isPause)
                _appointedTime = _remainingTime;
            
            IsPause = isPause;
        }

        public void SetCountingTimeWithoutRestart(float time)
        {
            if (!_isCountDown)
                return;

            _appointedTime = time;
            _remainingTime = _appointedTime;
        }

        private async UniTaskVoid CountDownAsync(bool isScaled, CancellationToken tokenForRequest,
            CancellationToken tokenOnDestroy = default)
        {
            _remainingTime = _appointedTime;
            _isCountDown = true;

            Started?.Invoke(_remainingTime);

            while (_remainingTime > 0)
            {
                if (tokenForRequest.IsCancellationRequested || tokenOnDestroy.IsCancellationRequested || !_isCountDown)
                {
                    InterruptedIncompleted?.Invoke();
                    return;
                }

                if (!IsPause)
                {
                    var deltaTime = isScaled ? Time.deltaTime : Time.unscaledDeltaTime;

                    if (_remainingTime % 1 > 0.9f && !_isUpdateInThisSecond)
                    {
                        UpdatedEverySeconds?.Invoke(_remainingTime);
                        _isUpdateInThisSecond = true;
                    }

                    else if (_isUpdateInThisSecond && _remainingTime % 1 <= 0.9f)
                        _isUpdateInThisSecond = false;

                    _remainingTime -= deltaTime;
                    Updated?.Invoke(_remainingTime);
                }

                await UniTask.DelayFrame(1, cancellationToken: tokenOnDestroy);
            }

            _remainingTime = 0;
            _isCountDown = false;
            Ended?.Invoke();
        }

        public static void StartCountingTime(CancellationToken token, float time, Action timeRanOut,
            bool isTimeScaled = true)
            => CountDownAsync(time, isTimeScaled, timeRanOut, token).Forget();

        private static async UniTaskVoid CountDownAsync(float time, bool isTimeScaled, Action timeRanOut,
            CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), !isTimeScaled, PlayerLoopTiming.Update, token);
            timeRanOut.Invoke();
        }
    }
}