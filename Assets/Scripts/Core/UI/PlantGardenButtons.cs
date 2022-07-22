using System;
using Core.Infrastructure;
using Zenject;

namespace Core.UI
{
    public class PlantGardenButtons : IInitializable, IDisposable
    {
        private readonly PlantGardenButtonsView _plantGardenButtonsView;

        public PlantGardenButtons(UISceneData uiSceneData) =>
            _plantGardenButtonsView = uiSceneData.PlantGardenButtonsView;

        public void Dispose()
        {
            _plantGardenButtonsView.GatherButton.onClick.RemoveListener(OnGatherButtonClick);
            _plantGardenButtonsView.WaterButton.onClick.RemoveListener(OnWaterButtonClick);
        }

        public void Initialize()
        {
            _plantGardenButtonsView.GatherButton.onClick.AddListener(OnGatherButtonClick);
            _plantGardenButtonsView.WaterButton.onClick.AddListener(OnWaterButtonClick);
        }
        
        public event Action GatherButtonClicked;
        public event Action WaterButtonClicked;
        
        public void ShowWaterButton()
        {
            _plantGardenButtonsView.WaterButton.gameObject.SetActive(true);
        }
        
        public void ShowGatherButton()
        {
            _plantGardenButtonsView.GatherButton.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _plantGardenButtonsView.WaterButton.gameObject.SetActive(false);
            _plantGardenButtonsView.GatherButton.gameObject.SetActive(false);
        }

        private void OnWaterButtonClick()
        {
            WaterButtonClicked?.Invoke();
        }

        private void OnGatherButtonClick()
        {
            GatherButtonClicked?.Invoke();
        }
    }
}