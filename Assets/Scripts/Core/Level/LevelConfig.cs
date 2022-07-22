using UnityEngine;

namespace Core.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game Configs/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private float _cubeSpawnInterval = 2f;
        [SerializeField] private float _cubeSpawnIntervalVariance = 1f;
        [SerializeField] private int _maxSpawnedCubes = 10;
        public float CubeSpawnInterval => _cubeSpawnInterval;
        public float CubeSpawnIntervalVariance => _cubeSpawnIntervalVariance;
        public int MaxSpawnedCubes => _maxSpawnedCubes;
    }
}