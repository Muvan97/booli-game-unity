using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class TonWalletConfig
    {
        [field: SerializeField] public float WithdrawalFeeInTon { get; private set; } = 0.5f;
        [field: SerializeField] public string TargetWalletAddress { get; private set; }
        [field: SerializeField] public Color CorrectSendingStateColor { get; private set; }
        [field: SerializeField] public Color IncorrectSendingStateColor { get; private set; }
    }
}