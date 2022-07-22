using System;
using UnityEngine;

namespace Core.Farming
{
    public class Plant
    {
        private readonly PlantView _plantView;

        public Plant(PlantView plantView)
        {
            _plantView = plantView;
            OutputFruitConfig = _plantView.OutputFruitConfig;

            _plantView.RipenStage.Hide();
        }

        public bool IsWatered { get; private set; }
        public FruitConfig OutputFruitConfig { get; }

        public Vector3 Position => _plantView.transform.position;

        public event Action StateChanged;

        public void Water()
        {
            IsWatered = true;
            _plantView.SproutStage.Hide();
            _plantView.RipenStage.AppearSmooth();

            StateChanged?.Invoke();
        }

        public void CollectFruit()
        {
            IsWatered = false;
            _plantView.RipenStage.Hide();
            _plantView.SproutStage.AppearImmediate();

            StateChanged?.Invoke();
        }
    }
}