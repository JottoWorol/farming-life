using Core.Level;
using Core.Player;
using UnityEngine;

namespace Core.Infrastructure
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _fruitFlyPoint;
        public LevelView LevelView => _levelView;
        public PlayerView PlayerView => _playerView;
        public Camera Camera => _camera;
        public Transform FruitFlyPoint => _fruitFlyPoint;
    }
}