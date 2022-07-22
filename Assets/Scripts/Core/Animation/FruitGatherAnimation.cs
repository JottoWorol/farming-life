using System;
using Core.Farming;
using Core.Infrastructure;
using Core.Player;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Animation
{
    public class FruitGatherAnimation : IInitializable, IDisposable
    {
        private readonly AnimationConfig _animationConfig;
        private readonly PlayerActions _playerActions;
        private readonly Transform _moneyPoint;
        private Camera _camera;
        
        public FruitGatherAnimation(StaticData staticData, PlayerActions playerActions, SceneData sceneData)
        {
            _playerActions = playerActions;
            _animationConfig = staticData.AnimationConfig;
            _moneyPoint = sceneData.FruitFlyPoint;
            _camera = sceneData.Camera;
        }

        public void PlayAnimation(Transform target, Vector3 targetPosition)
        {
            var startPosition = target.position;
            DOTween.Sequence()
                .Append(
                    DOTween.To(value =>
                            {
                                target.position = Parabola(startPosition, targetPosition, value);
                            }, startValue: 0, endValue: 1, _animationConfig.FruitGatherAnimationDuration
                        )
                        .SetEase(Ease.Linear).SetId(this)
                )
                .Append(target.DOMove(GetMoneyIndicatorWorldPoint(), _animationConfig.FruitGatherAnimationDuration))
                .AppendCallback(() => Object.Destroy(target.gameObject));
        }

        private Vector3 GetMoneyIndicatorWorldPoint()
        {
            return _moneyPoint.position;
        }
        
        private Vector3 Parabola(Vector3 start, Vector3 end, float t)
        {
            var height = _animationConfig.FruitGatherFlyHeight;
            float Func(float x) => _animationConfig.FruitGatherFlyParabolaMultiplier * (-height * x * x + height * x);
            var mid = Vector3.Lerp(start, end, t);
            return new Vector3(mid.x, Func(t) + mid.y, mid.z);
        }

        private void OnFruitGathered(Plant plant)
        {
            var target = Object.Instantiate(plant.OutputFruitConfig.ModelPrefab, plant.Position, Quaternion.identity)
                .transform;

            var distance = Random.insideUnitCircle * _animationConfig.FruitGatherMaxFlyRadius;
            var targetPosition = target.position + new Vector3(distance.x, 0, distance.y);
            
            PlayAnimation(target, targetPosition);
        }

        public void Initialize()
        {
            _playerActions.FruitGathered += OnFruitGathered;
        }

        public void Dispose()
        {
            _playerActions.FruitGathered -= OnFruitGathered;   
        }
    }
}