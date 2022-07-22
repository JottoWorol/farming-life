using System;
using DG.Tweening;
using UnityEngine;

namespace Core.Animation
{
    public class AnimatedAppearObject : MonoBehaviour
    {
        [SerializeField] private bool _hideOnStart = false;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _overshoot = 1.2f;

        private void Awake()
        {
            if (_hideOnStart)
            {
                Hide();
            }
        }

        public void AppearSmooth()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _scaleDuration).SetEase(_ease, _overshoot);
        }
        
        public void AppearImmediate()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.one;
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}