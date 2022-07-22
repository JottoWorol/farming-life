using UnityEngine;

namespace Core.Farming
{
    [CreateAssetMenu(fileName = "FruitConfig", menuName = "Game Configs/FruitConfig", order = 0)]
    public class FruitConfig : ScriptableObject
    {
        [SerializeField] private GameObject _modelPrefab;
        [SerializeField] private int _price = 10;

        public GameObject ModelPrefab => _modelPrefab;
        public int Price => _price;
    }
}