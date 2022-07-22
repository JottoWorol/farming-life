using Core.Farming;
using Core.Purchase;
using UnityEngine;

namespace Core.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private PurchasableItemView[] _purchasableItemViews;
        [SerializeField] private PlantGardenView[] _plantGardenViews;
        public PurchasableItemView[] PurchasableItemViews => _purchasableItemViews;
        public PlantGardenView[] PlantGardenViews => _plantGardenViews;
    }
}