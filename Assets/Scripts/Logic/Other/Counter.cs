
using System;

namespace Logic.Other
{
    public class Counter
    {
        public Action EndedCounting;
        public Action<long> Reduced;
        public long Count { get; private set; }

        public Counter(int count) => Count = count;
        public Counter(long count) => Count = count;
        
        public void Reduce(long number = 1)
        {
            if (Count == 0 || number <= 0)
                return;

            if (number > Count)
                number = Count;

            Count -= number;
            
            Reduced?.Invoke(Count);
            
            if (Count == 0)
                EndedCounting?.Invoke();
        }
    }
}