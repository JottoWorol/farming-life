using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class PlantGardenButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _waterButton;
        [SerializeField] private Button _gatherButton;

        public Button WaterButton => _waterButton;
        public Button GatherButton => _gatherButton;
    }
}