using System;
using Logic.Other;

namespace Infrastructure.Services
{
    public class CurrenciesProviderService : IService
    {
        public Currency Coins { get; private set; }
        public Currency Booli { get; private set; }

        private readonly GameDataProviderAndSaverService _saverService;
        
        public CurrenciesProviderService(GameDataProviderAndSaverService saver)
        {
            _saverService = saver;
        }

        public void InitializeCurrencies()
        {
            var currenciesData = _saverService.GameData.CurrenciesData;
            
            Coins = GetNewCurrency(currenciesData.NumberCoins, numberCoins => currenciesData.NumberCoins = numberCoins.ToString());
            Booli = GetNewCurrency(currenciesData.NumberBooli, numberBooli => currenciesData.NumberBooli = numberBooli.ToString());
        }

        private Currency GetNewCurrency(string numberCurrency, Action<decimal> saveAction)
        {
            var currency = new Currency(string.IsNullOrEmpty(numberCurrency) ? 0 : Convert.ToDecimal(numberCurrency));
            currency.NumberChanged += saveAction;
            //currency.NumberChanged += number => SaveData();
            return currency;
        }
    }
}