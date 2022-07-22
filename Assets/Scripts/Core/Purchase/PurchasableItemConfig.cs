using UnityEngine;

namespace Core.Purchase
{
    [CreateAssetMenu(fileName = "PurchasableItemConfig", menuName = "Game Configs/PurchasableItemConfig",
        order = 0
    )]
    public class PurchasableItemConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _price;
        
        public string Id => _id;
        public int Price => _price;
    }
}