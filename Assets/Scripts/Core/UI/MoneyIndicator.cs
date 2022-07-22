using System;
using Core.Economics;
using Core.Infrastructure;
using TMPro;
using Zenject;

namespace Core.UI
{
    public class MoneyIndicator : IInitializable, IDisposable
    {
        private readonly MoneyService _moneyService;
        private readonly TMP_Text _moneyText;
        
        public MoneyIndicator(MoneyService moneyService, UISceneData uiSceneData)
        {
            _moneyService = moneyService;
            _moneyText = uiSceneData.MoneyIndicatorText;
        }
        
        private void OnMoneyChanged()
        {
            _moneyText.SetText("{0:0}", _moneyService.MoneyValue);
        }

        public void Initialize()
        {
            _moneyService.ValueChanged += OnMoneyChanged;
            OnMoneyChanged();
        }

        public void Dispose()
        {
            _moneyService.ValueChanged -= OnMoneyChanged;
        }
    }
}