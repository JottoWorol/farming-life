using Core.Animation;
using UnityEngine;

namespace Core.Farming
{
    public class PlantView : MonoBehaviour
    {
        [SerializeField] private FruitConfig _outputFruitConfig;
        [SerializeField] private AnimatedAppearObject _sproutStage;
        [SerializeField] private AnimatedAppearObject _ripenStage;

        public AnimatedAppearObject SproutStage => _sproutStage;
        public AnimatedAppearObject RipenStage => _ripenStage;
        public FruitConfig OutputFruitConfig => _outputFruitConfig;
    }
}