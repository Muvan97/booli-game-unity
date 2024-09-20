using System;
using Logic.Other;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class SaveData : IIndexable
    {
        [field: SerializeField] public int Index { get; private set; }
        public int OpenLevel = 1;

        protected SaveData(int index, int openLevel)
        {
            Index = index;
            OpenLevel = openLevel;
        }
    }
}