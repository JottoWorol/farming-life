using UnityEngine;

namespace Core.Purchase
{
    [CreateAssetMenu(fileName = "PurchaseConfig", menuName = "Game Configs/PurchaseConfig", order = 0)]
    public class PurchaseConfig : ScriptableObject
    {
        [SerializeField] private int _moneyToSpendPerTake = 10;
        
        public int MoneyToSpendPerTake => _moneyToSpendPerTake;
    }
}