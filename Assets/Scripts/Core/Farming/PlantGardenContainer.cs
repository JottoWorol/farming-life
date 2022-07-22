using System;
using System.Collections.Generic;

namespace Core.Farming
{
    public class PlantGardenContainer : IDisposable
    {
        private readonly List<PlantGarden> _plantGardenList = new List<PlantGarden>();

        public void Dispose()
        {
            foreach (var plantGarden in _plantGardenList)
            {
                plantGarden.VisitedByPlayer -= OnPlantGardenVisitedByPlayer;
                plantGarden.LeftByPlayer -= OnPlantGardenLeftByPlayer;
                plantGarden.GardenStateChanged -= OnPlantGardenStateChanged;
            }
        }

        public void AddPlantGarden(PlantGarden plantGarden)
        {
            _plantGardenList.Add(plantGarden);
            plantGarden.VisitedByPlayer += OnPlantGardenVisitedByPlayer;
            plantGarden.LeftByPlayer += OnPlantGardenLeftByPlayer;
            plantGarden.GardenStateChanged += OnPlantGardenStateChanged;
        }

        public event Action<PlantGarden> PlantGardenVisitedByPlayer;
        public event Action<PlantGarden> PlantGardenLeftByPlayer;
        public event Action<PlantGarden> PlantGardenStateChanged;

        private void OnPlantGardenLeftByPlayer(PlantGarden plantGarden)
        {
            PlantGardenLeftByPlayer?.Invoke(plantGarden);
        }

        private void OnPlantGardenVisitedByPlayer(PlantGarden plantGarden)
        {
            PlantGardenVisitedByPlayer?.Invoke(plantGarden);
        }

        private void OnPlantGardenStateChanged(PlantGarden garden)
        {
            PlantGardenStateChanged?.Invoke(garden);
        }
    }
}