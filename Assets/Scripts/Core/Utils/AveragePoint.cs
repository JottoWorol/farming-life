using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    public class AveragePoint : MonoBehaviour
    {
        [SerializeField] private List<Transform> _point;
        private void FixedUpdate()
        {
            var average = Vector3.zero;
            foreach (var point in _point)
            {
                average += point.position;
            }
            average /= _point.Count;
            
            transform.position = average;
        }
    }
}