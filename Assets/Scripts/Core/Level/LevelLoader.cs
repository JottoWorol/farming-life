using Core.Farming;
using Core.Infrastructure;
using Core.Player;
using Core.Purchase;
using Zenject;

namespace Core.Level
{
    public class LevelLoader : IInitializable
    {
        private readonly LevelView _levelView;
        private readonly PurchaseService _purchaseService;
        private readonly PlantGardenContainer _plantGardenContainer;

        public LevelLoader(SceneData sceneData, PurchaseService purchaseService, PlantGardenContainer plantGardenContainer)
        {
            _purchaseService = purchaseService;
            _plantGardenContainer = plantGardenContainer;
            _levelView = sceneData.LevelView;
        }

        public void Initialize()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            foreach (var purchasableItemView in _levelView.PurchasableItemViews)
            {
                var purchasableItem = new PurchasableItem(purchasableItemView);
                _purchaseService.Add(purchasableItem);
            }
            
            _purchaseService.Initialize();

            foreach (var gardenView in _levelView.PlantGardenViews)
            {
                var plantGarden = new PlantGarden(gardenView);
                _plantGardenContainer.AddPlantGarden(plantGarden);
            }
        }
    }
}