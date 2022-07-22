using System;
using UnityEngine;

namespace Core.Economics
{
    public class MoneyService
    {
        private const string MoneyValueKey = nameof(MoneyValue);

        public int MoneyValue
        {
            get => PlayerPrefs.GetInt(MoneyValueKey, 10);
            private set => PlayerPrefs.SetInt(MoneyValueKey, value);
        }

        public event Action ValueChanged;

        public void AddMoney(int value)
        {
            MoneyValue += value;
            ValueChanged?.Invoke();
        }

        public bool TrySpend(int amount)
        {
            if(amount <= MoneyValue)
            {
                MoneyValue -= amount;
                ValueChanged?.Invoke();
                return true;
            }
            
            return false;
        }
    }
}