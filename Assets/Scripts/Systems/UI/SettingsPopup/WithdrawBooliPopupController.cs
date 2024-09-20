using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Holders;
using Newtonsoft.Json;
using RestApiSystem;
using TonSdk.Connect;
using Tools;
using UnitonConnect.Core.Utils.Debugging;
using UnityEngine;

namespace Systems.UI.SettingsPopup
{
    public class WithdrawBooliPopupController
    {
        private readonly WithdrawBooliPopupView _view;
        private readonly WithdrawBooliPopupModel _model;
        private readonly TonWalletConnectionModel _tonWalletConnectionModel;

        public WithdrawBooliPopupController(WithdrawBooliPopupView view, WithdrawBooliPopupModel model,
            TonWalletConnectionModel tonWalletConnectionModel)
        {
            _view = view;
            _model = model;
            _tonWalletConnectionModel = tonWalletConnectionModel;
            UITools.InitializeCoinsCounterView(_view.BooliCounterView, _model.CurrenciesProviderService.Booli);

            InitializeCommisionText().Forget();

            _view.Observer.Enabled += OnObserverEnable;
            _view.Observer.Disabled += OnObserverDisable;

            Initialize();

            _view.SendTonButton.onClick.AddListener(() => TrySendTon().Forget());
        }

        private async void Initialize()
        {
            _view.SetTonBalanceText(_model.UnitonConnectSdk.TonBalance);

            var isPlayerSentBooli = _model.GameDataProviderAndSaverService.IsPlayerSentBooli;

            _view.SendTonButton.enabled = !isPlayerSentBooli;

            _view.SendingStateText.text = isPlayerSentBooli
                ? await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.SentWait, LocalizationTable.WithdrawBooliPopup)
                : string.Empty;

            if (isPlayerSentBooli)
                _view.SendingStateText.color = _model.GameConfig.TonWalletConfig.CorrectSendingStateColor;
        }

        private async UniTask InitializeCommisionText()
        {
            _view.CommissionText.text = await LocalizationTools.GetLocalizedString(LocalizationKeysHolder.Commission,
                                            LocalizationTable.WithdrawBooliPopup) + ": " +
                                        _model.GameConfig.TonWalletConfig.WithdrawalFeeInTon + " TON";
        }

        private async UniTaskVoid TrySendTon()
        {
            if (!IsCanSendBooli(out var amount, out var error))
            {
                _view.SendingStateText.text = error == string.Empty
                    ? string.Empty
                    : await LocalizationTools.GetLocalizedString(error, LocalizationTable.WithdrawBooliPopup);
                _view.SendingStateText.color = _model.GameConfig.TonWalletConfig.IncorrectSendingStateColor;
                return;
            }

            _view.SendingStateText.text = string.Empty;
            _model.LastInputtedBooliAmount = amount;
            var latestWallet = _tonWalletConnectionModel.LatestAuthorizedWallet;

            try
            {
                await _model.UnitonConnectSdk.SendTon(latestWallet,
                    _model.GameConfig.TonWalletConfig.TargetWalletAddress,
                    _model.GameConfig.TonWalletConfig.WithdrawalFeeInTon);
            }
            catch (Exception exception)
            {
                _view.SendingStateText.text = await LocalizationTools.GetLocalizedString(
                    LocalizationKeysHolder.UnsuccessfulOperation, LocalizationTable.WithdrawBooliPopup);
                _view.SendingStateText.color = _model.GameConfig.TonWalletConfig.IncorrectSendingStateColor;
                
                Debug.LogError(exception);
            }
        }

        private bool IsCanSendBooli(out long amount, out string error)
        {
            error = string.Empty;

            if (!long.TryParse(_view.AmountInputField.text, out amount) || amount <= 0)
            {
                error = LocalizationKeysHolder.IncorrentBooliAmount;
                return false;
            }

            if (amount > _model.CurrenciesProviderService.Booli.Number)
            {
                error = LocalizationKeysHolder.NotEnoughBooli;
                return false;
            }

            return true;
        }

        private void OnObserverEnable()
        {
            _model.UnitonConnectSdk.UpdateTonBalance();

            _model.UnitonConnectSdk.OnSendingTonFinished += OnTransactionSendingFinish;
            _model.UnitonConnectSdk.OnTonBalanceClaimed += OnTonBalanceClaim;
        }

        private void OnObserverDisable()
        {
            _model.UnitonConnectSdk.OnSendingTonFinished -= OnTransactionSendingFinish;
            _model.UnitonConnectSdk.OnTonBalanceClaimed -= OnTonBalanceClaim;
        }

        private void OnTonBalanceClaim(decimal tonBalance) => _view.SetTonBalanceText(tonBalance);

        private async void OnTransactionSendingFinish(
            SendTransactionResult? result, bool isSuccess)
        {
            var isSent = true;

            _view.SendingStateText.text = await LocalizationTools.GetLocalizedString(
                isSent
                    ? LocalizationKeysHolder.SentWait
                    : LocalizationKeysHolder.UnsuccessfulOperation, LocalizationTable.WithdrawBooliPopup);

            _view.SendingStateText.color = isSent
                ? _model.GameConfig.TonWalletConfig.CorrectSendingStateColor
                : _model.GameConfig.TonWalletConfig.IncorrectSendingStateColor;

            if (!isSent)
            {
                UnitonConnectLogger.LogError("Failed to send transaction for possible reasons:" +
                                             " not enough funds or unsuccessful connection to the wallet");
                
                return;
            }

            _view.SendTonButton.enabled = false;

            _model.CurrenciesProviderService.Booli.Number -= _model.LastInputtedBooliAmount;
            _model.GameDataProviderAndSaverService.IsPlayerSentBooli = true;

            var fields = new Dictionary<string, string>
            {
                {RequestFieldNames.Identifier, _model.GameDataProviderAndSaverService.PlayerID},
                {
                    RequestFieldNames.PlayerAccountData, JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        {
                            RequestFieldNames.BooliWithdrawInformation, JsonConvert.SerializeObject(
                                new Dictionary<string, string>
                                {
                                    {RequestFieldNames.WalletAddress, _model.UnitonConnectSdk.GetWalletAddress()},
                                    {RequestFieldNames.NumberSentBooli, _model.LastInputtedBooliAmount.ToString()}
                                })
                        }
                    })
                }
            };
            
            await _model.RestApiMediatorService.GetProcessedRequestResult(Request.Post, fields);
        }
    }
}