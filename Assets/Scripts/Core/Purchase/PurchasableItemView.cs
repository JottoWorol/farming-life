using System.Collections.Generic;
using Core.Animation;
using Core.Player;
using TMPro;
using UnityEngine;

namespace Core.Purchase
{
    public class PurchasableItemView : MonoBehaviour
    {
        [SerializeField] private PurchasableItemConfig _config;
        [SerializeField] private GameObject _priceObject;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private PlayerDetector _playerDetector;
        [SerializeField] private List<AnimatedAppearObject> _objectsToDisable;
        [SerializeField] private List<AnimatedAppearObject> _objectsToEnable;
        
        public GameObject PriceObject => _priceObject;
        public TMP_Text PriceText => _priceText;
        public PlayerDetector PlayerDetector => _playerDetector;
        public List<AnimatedAppearObject> ObjectsToDisable => _objectsToDisable;
        public List<AnimatedAppearObject> ObjectsToEnable => _objectsToEnable;
        public PurchasableItemConfig Config => _config;
    }
}