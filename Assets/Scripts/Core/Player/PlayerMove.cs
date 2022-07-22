using System;
using Core.Infrastructure;
using Core.Joystick;
using UnityEngine;
using Zenject;

namespace Core.Player
{
    public class PlayerMove : IInitializable, IDisposable, IFixedTickable
    {
        private readonly PlayerView _playerView;
        private readonly MoveJoystick _moveJoystick;
        private readonly PlayerConfig _playerConfig;
        
        public PlayerMove(MoveJoystick moveJoystick, StaticData staticData, SceneData sceneData)
        {
            _moveJoystick = moveJoystick;
            _playerConfig = staticData.PlayerConfig;
            _playerView = sceneData.PlayerView;
        }

        public event Action StartedMoving;
        public event Action StoppedMoving;

        public void Initialize()
        {
            _moveJoystick.StartedMoving += OnStartedMoving;
            _moveJoystick.StoppedMoving += OnStoppedMoving;
        }
        
        public void Dispose()
        {
            _moveJoystick.StartedMoving -= OnStartedMoving;
            _moveJoystick.StoppedMoving -= OnStoppedMoving;
        }

        private bool _isMoving;

        private void OnStoppedMoving()
        {
            _isMoving = false;

            if (_playerView == null)
                return;
         
            PlayerRigidbody.velocity = Vector3.zero;
            PlayerRigidbody.angularVelocity = Vector3.zero;
            StoppedMoving?.Invoke();
        }

        private void OnStartedMoving()
        {
            _isMoving = true;
            StartedMoving?.Invoke();
        }
        
        private Rigidbody PlayerRigidbody => _playerView.Rigidbody;
        
        public void FixedTick()
        {
            if(!_isMoving || _playerView == null)
                return;
            
            var joystickValue = _moveJoystick.Value;
            var velocity = new Vector3(joystickValue.x, 0, joystickValue.y) * _playerConfig.MoveSpeed;

            if(velocity.magnitude == 0)
                return;

            PlayerRigidbody.velocity = velocity;
            PlayerRigidbody.rotation = Quaternion.RotateTowards(
                PlayerRigidbody.rotation,
                Quaternion.LookRotation(velocity, Vector3.up), 
                _playerConfig.RotationSpeed
            );
        }
    }
}