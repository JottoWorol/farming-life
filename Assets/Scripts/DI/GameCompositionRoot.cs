using Core.Animation;
using Core.Economics;
using Core.Farming;
using Core.Infrastructure;
using Core.Joystick;
using Core.Level;
using Core.Player;
using Core.Purchase;
using Core.UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameCompositionRoot : MonoInstaller
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private StaticData _staticData;
        [SerializeField] private UISceneData _uiSceneData;
        
        public override void InstallBindings()
        {
            BindInfrastructure();
            
            Container.BindSingle<MoveJoystick>().NonLazy();
            Container.BindSingle<PlayerMove>();
            Container.BindSingle<LevelLoader>();
            Container.BindSingle<PlayerAnimation>();
            Container.BindSingle<PurchaseService>();
            Container.BindSingle<MoneyService>();
            Container.BindSingle<PlayerActions>();
            Container.BindSingle<PlantGardenContainer>();
            Container.BindSingle<PlantGardenButtons>();
            Container.BindSingle<GardenButtonsVisibility>();
            Container.BindSingle<FruitGatherAnimation>();
            Container.BindSingle<MoneyIndicator>();
        }

        private void BindInfrastructure()
        {
            Container.BindInstance(_sceneData);
            Container.BindInstance(_staticData);
            Container.BindInstance(_uiSceneData);
        }

    }
}