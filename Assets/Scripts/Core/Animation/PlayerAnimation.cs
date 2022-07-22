using System;
using Core.Infrastructure;
using Core.Player;
using UnityEngine;
using Zenject;

namespace Core.Animation
{
    public class PlayerAnimation : IInitializable, IDisposable
    {
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Run = Animator.StringToHash("run");
        
        private readonly PlayerMove _playerMove;
        private readonly Animator _animator;
        private static readonly int Watering = Animator.StringToHash("watering");
        private static readonly int Gathering = Animator.StringToHash("gathering");
        private static readonly int Actions = Animator.StringToHash("stop_actions");

        public PlayerAnimation(PlayerMove playerMove, SceneData sceneData)
        {
            _playerMove = playerMove;
            _animator = sceneData.PlayerView.Animator;
        }

        public void Dispose()
        {
            _playerMove.StartedMoving -= OnStartedMoving;
            _playerMove.StoppedMoving -= OnStoppedMoving;
        }

        public void Initialize()
        {
            _playerMove.StartedMoving += OnStartedMoving;
            _playerMove.StoppedMoving += OnStoppedMoving;
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }

        public void PlayRun()
        {
            _animator.SetTrigger(Run);
        }
        
        public void SetWatering()
        {
            _animator.SetTrigger(Watering);
        }
        
        public void SetGathering()
        {
            _animator.SetTrigger(Gathering);
        }

        public void StopActions()
        {
            _animator.SetTrigger(Actions);
        }

        private void OnStartedMoving()
        {
            if (_animator == null)
                return;

            PlayRun();
        }

        private void OnStoppedMoving()
        {
            if (_animator == null)
                return;

            PlayIdle();
        }
    }
}