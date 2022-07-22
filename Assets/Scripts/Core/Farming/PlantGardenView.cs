using Core.Player;
using UnityEngine;

namespace Core.Farming
{
    public class PlantGardenView : MonoBehaviour
    {
        [SerializeField] private PlantView[] _plantViews;
        [SerializeField] private PlayerDetector _playerDetector;
        
        public PlantView[] PlantViews => _plantViews;
        public PlayerDetector PlayerDetector => _playerDetector;
    }
}