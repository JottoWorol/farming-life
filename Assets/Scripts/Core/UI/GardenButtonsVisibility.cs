using System;
using Core.Farming;
using Zenject;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace Core.UI
{
    public class GardenButtonsVisibility : IInitializable, IDisposable
    {
        private readonly PlantGardenContainer _plantGardenContainer;
        private readonly PlantGardenButtons _plantGardenButtons;
        
        public GardenButtonsVisibility(PlantGardenContainer plantGardenContainer, PlantGardenButtons plantGardenButtons)
        {
            _plantGardenContainer = plantGardenContainer;
            _plantGardenButtons = plantGardenButtons;
        }

        public void Initialize()
        {
            _plantGardenContainer.PlantGardenVisitedByPlayer += OnPlantGardenVisitedByPlayer;
            _plantGardenContainer.PlantGardenLeftByPlayer += OnPlantGardenLeftByPlayer;
            _plantGardenContainer.PlantGardenStateChanged += OnPlantGardenStateChanged;
        }

        public void Dispose()
        {
            _plantGardenContainer.PlantGardenVisitedByPlayer -= OnPlantGardenVisitedByPlayer;
            _plantGardenContainer.PlantGardenLeftByPlayer -= OnPlantGardenLeftByPlayer;
            _plantGardenContainer.PlantGardenStateChanged -= OnPlantGardenStateChanged;
        }

        private void OnPlantGardenStateChanged(PlantGarden garden)
        {
            _plantGardenButtons.Hide();
            OnPlantGardenVisitedByPlayer(garden);
        }

        private void OnPlantGardenVisitedByPlayer(PlantGarden plantGarden)
        {
            switch (plantGarden.CurrentStage)
            {
                case PlantGarden.GardenStage.Watering:
                    _plantGardenButtons.ShowWaterButton();
                    break;
                case PlantGarden.GardenStage.Gathering:
                    _plantGardenButtons.ShowGatherButton();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnPlantGardenLeftByPlayer(PlantGarden plantGarden)
        {
            _plantGardenButtons.Hide();
        }
    }
}