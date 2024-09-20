using System;
using Logic.Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.UI
{
    public class Holdable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler
    {
        public Action<float> TimerUpdated;
        public Action Clamped;
        public Action EndedClamping;
        public Action WasNotPressedToEnd;
        public Action BeginDrag;

        public bool IsClamped { get; private set; }
        public float ClampingTime { get; private set; }
        [field: SerializeField] public float TimeBeforeAction { get; private set; }

        protected bool isActionTakingPlace;

        [SerializeField] private bool _isStopHoldingOnBeginDrag;
        private Timer _timer;


        protected void Start()
        {
            _timer = new Timer(destroyCancellationToken);
            Subscribe();
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isStopHoldingOnBeginDrag)
                StopHolding();
        
            BeginDrag?.Invoke();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
            => _timer.StartCountingTime(TimeBeforeAction);

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            isActionTakingPlace = false;
            IsClamped = false;
            EndedClamping?.Invoke();
            StopHolding();
        }

        private void Subscribe()
        {
            Clamped += OnClamp;
            _timer.Updated += time => ClampingTime = time;
            _timer.Updated += time => TimerUpdated?.Invoke(time);
            _timer.InterruptedIncompleted += OnInterruptedIncomplete;

            _timer.Ended += () => isActionTakingPlace = true;
            _timer.Ended += () => Clamped?.Invoke();
        }

        protected virtual void OnClamp() => IsClamped = true;

        private void OnInterruptedIncomplete() => WasNotPressedToEnd?.Invoke();

        private void StopHolding() => _timer.StopCountingTime();
    }
}