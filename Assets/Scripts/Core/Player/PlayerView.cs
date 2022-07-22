using UnityEngine;

namespace Core.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _moveTransform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _wateringCan;
        [SerializeField] private GameObject _scythe;
        [SerializeField] private PlantDetector _plantDetector;

        public Animator Animator => _animator;
        public Transform MoveTransform => _moveTransform;
        public Rigidbody Rigidbody => _rigidbody;
        public GameObject WateringCan => _wateringCan;
        public GameObject Scythe => _scythe;
        public PlantDetector PlantDetector => _plantDetector;
    }
}