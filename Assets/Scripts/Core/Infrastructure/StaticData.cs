using Core.Animation;
using Core.Joystick;
using Core.Level;
using Core.Player;
using Core.Purchase;
using UnityEngine;

namespace Core.Infrastructure
{
    public class StaticData : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private JoystickConfig _joystickConfig;
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private PurchaseConfig _purchaseConfig;
        [SerializeField] private AnimationConfig _animationConfig;

        public PlayerConfig PlayerConfig => _playerConfig;
        public JoystickConfig JoystickConfig => _joystickConfig;
        public LevelConfig LevelConfig => _levelConfig;
        public PurchaseConfig PurchaseConfig => _purchaseConfig;
        public AnimationConfig AnimationConfig => _animationConfig;
    }
}