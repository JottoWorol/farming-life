using System;
using System.Collections.Generic;
using Core.Economics;
using Core.Infrastructure;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Core.Purchase
{
    public class PurchaseService : IDisposable, ITickable
    {
        private readonly MoneyService _moneyService;
        private readonly PurchaseConfig _purchaseConfig;
        
        private const string PurchaseDataStringKey = nameof(PurchaseDataString);
        private readonly Dictionary<string, IPurchasableItem> _idToItem = new Dictionary<string, IPurchasableItem>();
        private Dictionary<string, bool> _isItemPurchased = new Dictionary<string, bool>();
        private Dictionary<string, int> _moneyToPay = new Dictionary<string, int>();

        public PurchaseService(MoneyService moneyService, StaticData staticData)
        {
            _moneyService = moneyService;
            _purchaseConfig = staticData.PurchaseConfig;
        }

        public string PurchaseDataString
        {
            get => PlayerPrefs.GetString(PurchaseDataStringKey, "");
            private set => PlayerPrefs.SetString(PurchaseDataStringKey, value);
        }

        private void Purchase(string id)
        {
            _isItemPurchased[id] = true;
            _idToItem[id].Purchase();
            SavePurchaseData();
        }

        public void Add(IPurchasableItem item)
        {
            _idToItem[item.PurchaseItemId] = item;
            _moneyToPay[item.PurchaseItemId] = item.Price;
            _isItemPurchased[item.PurchaseItemId] = false;
        }

        public void Initialize()
        {
            RetrieveSavedPurchasedData();
            
            foreach (var item in _idToItem.Values)
            {
                item.SetMoneyToSpend(_moneyToPay[item.PurchaseItemId]);
            }
        }

        public bool IsPurchased(string id) => _isItemPurchased.ContainsKey(id) && _isItemPurchased[id];
        
        private void SavePurchaseData()
        {
            var saveData = new PurchaseData
            {
                IsItemPurchased = _isItemPurchased,
                MoneyToPay = _moneyToPay,
            };
            PurchaseDataString = JsonConvert.SerializeObject(saveData);
        }

        private void RetrieveSavedPurchasedData()
        {
            var savedData = JsonConvert.DeserializeObject<PurchaseData>(PurchaseDataString);
            if (savedData == null)
                return;

            _isItemPurchased = savedData.IsItemPurchased;
            _moneyToPay = savedData.MoneyToPay;

            foreach (var key in _isItemPurchased.Keys)
            {
                if (_isItemPurchased[key])
                    _idToItem[key].PurchaseImmediately();
            }
        }

        public void Dispose()
        {
            foreach (var item in _idToItem.Values)
            {
                item.OnDispose();
            }
        }

        public void Tick()
        {
            foreach (var item in _idToItem.Values)
            {
                if (!item.IsPlayerInsideDetector 
                    || _isItemPurchased[item.PurchaseItemId]
                    || _moneyService.MoneyValue <= 0)
                    continue;

                var moneyToSpend = Mathf.Min(
                    _moneyToPay[item.PurchaseItemId], 
                        _moneyService.MoneyValue, 
                        _purchaseConfig.MoneyToSpendPerTake);

                _moneyService.TrySpend(moneyToSpend);
                _moneyToPay[item.PurchaseItemId] -= moneyToSpend;

                var moneyToPay = _moneyToPay[item.PurchaseItemId];
                item.SetMoneyToSpend(moneyToPay);
                
                if(moneyToPay <= 0)
                    Purchase(item.PurchaseItemId);
            }
        }
    }

    [Serializable]
    public class PurchaseData
    {
        public Dictionary<string, bool> IsItemPurchased = new Dictionary<string, bool>();
        public Dictionary<string, int> MoneyToPay = new Dictionary<string, int>();
    }
}