using UnityEngine;

namespace Core.Animation
{
    [CreateAssetMenu(fileName = "AnimationConfig", menuName = "Game Configs/AnimationConfig", order = 0)]
    public class AnimationConfig : ScriptableObject
    {
        [SerializeField] private float _fruitGatherAnimationDuration = 0.5f;
        [SerializeField] private float _fruitGatherFlyHeight = 1f;
        [SerializeField] private float _fruitGatherFlyParabolaMultiplier = 4f;
        [SerializeField] private float _fruitGatherMaxFlyRadius = 1f;

        public float FruitGatherAnimationDuration => _fruitGatherAnimationDuration;
        public float FruitGatherFlyHeight => _fruitGatherFlyHeight;
        public float FruitGatherFlyParabolaMultiplier => _fruitGatherFlyParabolaMultiplier;
        public float FruitGatherMaxFlyRadius => _fruitGatherMaxFlyRadius;
    }
}