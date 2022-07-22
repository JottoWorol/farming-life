using System;
using UnityEngine;

namespace Core.Player
{
    public class PlayerDetector : MonoBehaviour
    {
        public event Action PlayerWentInside;
        public event Action PlayerWentOutside;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerView _))
                PlayerWentInside?.Invoke();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out PlayerView _))
                PlayerWentOutside?.Invoke();
        }
    }
}