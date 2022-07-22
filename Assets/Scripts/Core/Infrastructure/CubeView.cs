using UnityEngine;

namespace Core.Infrastructure
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 0.2f;
        public Transform Transform => transform;
        public float MoveDuration => _moveDuration;
    }
}