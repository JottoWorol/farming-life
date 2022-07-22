using Core.Joystick;
using Core.UI;
using TMPro;
using UnityEngine;

namespace Core.Infrastructure
{
    public class UISceneData : MonoBehaviour
    {
        [SerializeField] private ScreenTouchView _screenTouchView;
        [SerializeField] private JoystickView _joystickView;
        [SerializeField] private PlantGardenButtonsView _plantGardenButtonsView;
        [SerializeField] private TMP_Text _moneyIndicatorText;
        public ScreenTouchView ScreenTouchView => _screenTouchView;
        public JoystickView JoystickView => _joystickView;
        public PlantGardenButtonsView PlantGardenButtonsView => _plantGardenButtonsView;
        public TMP_Text MoneyIndicatorText => _moneyIndicatorText;
    }
}