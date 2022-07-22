using System;
using Core.Animation;
using Core.Economics;
using Core.Farming;
using Core.Infrastructure;
using Core.UI;
using Zenject;

namespace Core.Player
{
    public class PlayerActions : IInitializable, IDisposable
    {
        private readonly PlantDetector _plantDetector;
        private readonly PlantGardenButtons _plantGardenButtons;
        private readonly PlantGardenContainer _plantGardenContainer;
        private readonly PlayerAnimation _playerAnimation;
        private readonly PlayerView _playerView;
        private bool _isActing;
        private readonly MoneyService _moneyService;

        private PlantGarden _targetGarden;

        public PlayerActions(SceneData sceneData, PlayerAnimation playerAnimation,
            PlantGardenContainer plantGardenContainer, PlantGardenButtons plantGardenButtons, MoneyService moneyService)
        {
            _playerView = sceneData.PlayerView;
            _plantDetector = sceneData.PlayerView.PlantDetector;
            _playerAnimation = playerAnimation;
            _plantGardenContainer = plantGardenContainer;
            _plantGardenButtons = plantGardenButtons;
            _moneyService = moneyService;
        }

        public void Dispose()
        {
            _plantGardenButtons.WaterButtonClicked -= StartWatering;
            _plantGardenButtons.GatherButtonClicked -= StartGathering;
            _plantGardenContainer.PlantGardenLeftByPlayer -= OnPlantGardenLeftByPlayer;
            _plantGardenContainer.PlantGardenVisitedByPlayer -= OnPlantGardenVisitedByPlayer;
            _plantGardenContainer.PlantGardenStateChanged -= OnPlantGardenStateChanged;
            _plantDetector.PlantListAdded -= OnPlantListChanged;
        }

        public void Initialize()
        {
            _plantGardenButtons.WaterButtonClicked += StartWatering;
            _plantGardenButtons.GatherButtonClicked += StartGathering;
            _plantGardenContainer.PlantGardenLeftByPlayer += OnPlantGardenLeftByPlayer;
            _plantGardenContainer.PlantGardenVisitedByPlayer += OnPlantGardenVisitedByPlayer;
            _plantGardenContainer.PlantGardenStateChanged += OnPlantGardenStateChanged;
            _plantDetector.PlantListAdded += OnPlantListChanged;
        }

        public event Action<Plant> FruitGathered;

        private void StartWatering()
        {
            _playerView.Scythe.SetActive(false);
            _playerView.WateringCan.SetActive(true);
            _playerAnimation.SetWatering();
            _isActing = true;
            OnPlantListChanged();
        }

        private void StartGathering()
        {
            _playerView.WateringCan.SetActive(false);
            _playerView.Scythe.SetActive(true);
            _playerAnimation.SetGathering();
            _isActing = true;
            OnPlantListChanged();
        }

        private void StopActions()
        {
            _playerView.Scythe.SetActive(false);
            _playerView.WateringCan.SetActive(false);
            _playerAnimation.StopActions();
            _isActing = false;
        }

        private void OnPlantGardenStateChanged(PlantGarden garden)
        {
            StopActions();
        }

        private void OnPlantGardenLeftByPlayer(PlantGarden garden)
        {
            StopActions();
            _targetGarden = null;
        }

        private void OnPlantGardenVisitedByPlayer(PlantGarden plantGarden)
        {
            _targetGarden = plantGarden;
        }

        private void OnPlantListChanged()
        {
            if (!_isActing || _targetGarden == null)
                return;

            foreach (var plantView in _plantDetector.Plants)
            {
                if (!_targetGarden.TryGetPlantByView(plantView, out var plant))
                    continue;

                switch (_targetGarden.CurrentStage)
                {
                    case PlantGarden.GardenStage.Watering:
                        if (!plant.IsWatered)
                            plant.Water();
                        break;
                    case PlantGarden.GardenStage.Gathering:
                        if (plant.IsWatered)
                        {
                            plant.CollectFruit();
                            _moneyService.AddMoney(plant.OutputFruitConfig.Price);
                            FruitGathered?.Invoke(plant);
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}