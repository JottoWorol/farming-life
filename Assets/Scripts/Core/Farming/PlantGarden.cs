using System;
using System.Collections.Generic;

namespace Core.Farming
{
    public class PlantGarden
    {
        public enum GardenStage
        {
            Watering,
            Gathering,
        }

        private readonly PlantGardenView _plantGardenView;

        private readonly List<Plant> _plants = new List<Plant>();
        private readonly Dictionary<PlantView, Plant> _plantViewToPlant = new Dictionary<PlantView, Plant>();
        private bool _isPlayerInside;

        public PlantGarden(PlantGardenView plantGardenView)
        {
            _plantGardenView = plantGardenView;

            InitializeGarden();
        }

        public GardenStage CurrentStage { get; private set; }

        public event Action<PlantGarden> VisitedByPlayer;
        public event Action<PlantGarden> LeftByPlayer;
        public event Action<PlantGarden> GardenStateChanged;

        public bool TryGetPlantByView(PlantView view, out Plant plant)
            => _plantViewToPlant.TryGetValue(view, out plant);

        private void InitializeGarden()
        {
            foreach (var plantView in _plantGardenView.PlantViews)
            {
                var plant = new Plant(plantView);
                _plants.Add(plant);
                _plantViewToPlant.Add(plantView, plant);

                plant.StateChanged += OnPlantStateChanged;
            }

            CurrentStage = GardenStage.Watering;
            _plantGardenView.PlayerDetector.PlayerWentInside += OnPlayerWentInside;
            _plantGardenView.PlayerDetector.PlayerWentOutside += OnPlayerWentOutside;
        }

        private void OnPlayerWentInside()
        {
            if (!_isPlayerInside)
                VisitedByPlayer?.Invoke(this);

            _isPlayerInside = true;
        }

        private void OnPlayerWentOutside()
        {
            if (_isPlayerInside)
                LeftByPlayer?.Invoke(this);

            _isPlayerInside = false;
        }

        private void OnPlantStateChanged()
        {
            var hasWatered = false;
            var hasUnwatered = false;

            foreach (var plant in _plants)
            {
                if (plant.IsWatered)
                    hasWatered = true;
                else
                    hasUnwatered = true;
            }

            if (CurrentStage == GardenStage.Watering && hasWatered && !hasUnwatered)
            {
                CurrentStage = GardenStage.Gathering;
                GardenStateChanged?.Invoke(this);
            }
            else if (CurrentStage == GardenStage.Gathering && !hasWatered && hasUnwatered)
            {
                CurrentStage = GardenStage.Watering;
                GardenStateChanged?.Invoke(this);
            }
        }

        public void OnDispose()
        {
            foreach (var plant in _plants)
            {
                plant.StateChanged -= OnPlantStateChanged;
            }

            _plantGardenView.PlayerDetector.PlayerWentInside -= OnPlayerWentInside;
            _plantGardenView.PlayerDetector.PlayerWentOutside -= OnPlayerWentOutside;
        }
    }
}