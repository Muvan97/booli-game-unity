using System;

namespace Data
{
    [Serializable]
    public class UpgradeData : SaveData
    {
        public UpgradeData(int index, int openLevel) : base(index, openLevel)
        {
        }
    }
}