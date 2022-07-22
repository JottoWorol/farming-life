using UnityEngine;

namespace Core.Joystick
{
    [CreateAssetMenu(fileName = "JoystickConfig", menuName = "Game Configs/JoystickConfig", order = 0)]
    public class JoystickConfig : ScriptableObject
    {
        [SerializeField] private float _moveThreshold = 0.05f;
        [SerializeField] private float _radiusPixels = 100f;

        public float MoveThreshold => _moveThreshold;
        public float RadiusPixels => _radiusPixels;
    }
}