using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed = 1f;

        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
    }
}