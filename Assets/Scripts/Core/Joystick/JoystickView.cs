using UnityEngine;

namespace Core.Joystick
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private RectTransform _backRect;
        [SerializeField] private RectTransform _moveRect;
        [SerializeField] private float _maxMoveRectRadius = 66f;

        public void SetPosition(Vector2 position)
        {
            _backRect.anchoredPosition = position;
        }

        public void SetMovePosition(Vector2 position)
        {
            _moveRect.anchoredPosition =
                new Vector2(position.x * _maxMoveRectRadius, position.y * _maxMoveRectRadius);
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}