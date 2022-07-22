using System;
using System.Collections.Generic;
using Core.Farming;
using UnityEngine;

namespace Core.Player
{
    public class PlantDetector : MonoBehaviour
    {
        public List<PlantView> Plants;
        public event Action PlantListAdded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlantView view))
            {
                Plants.Add(view);
                PlantListAdded?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlantView view))
            {
                Plants.Remove(view);
            }
        }
    }
}