using System;

namespace Logic.Other
{
    public class Currency
    {
        public Currency(decimal number) => Number = number;

        public decimal Number
        {
            get => _number;
            set
            {
                if (value < 0)
                    return;

                _number = value;
                NumberChanged?.Invoke(value);
            }
        }

        private decimal _number;

        public Action<decimal> NumberChanged;
    }
}