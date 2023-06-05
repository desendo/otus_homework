using Custom.Config;
using Custom.Controllers;
using Custom.Data;
using Custom.DependencyInjection;
using Custom.GameManager;
using Custom.SaveLoadHandlers;
using Custom.Services;
using Custom.Signals;
using Custom.View;
using Custom.View.PresentationModel;
using UnityEngine;

namespace Custom
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private GameSaveDataContainer _gameSaveDataContainer;
        [SerializeField] private VisualConfig _visualConfig;
        [SerializeField] private GameConfig _gameConfig;

        private DependencyContainer _dependencyContainer;
        private void Start()
        {
            _dependencyContainer = new DependencyContainer();

            Bind();
            Initialize();
        }
        private void Bind()
        {
            _dependencyContainer.Add(_dependencyContainer);

            //конфиги и похожие вещи
            _dependencyContainer.Add(_gameSaveDataContainer);
            _dependencyContainer.Add(_visualConfig);
            _dependencyContainer.Add(_gameConfig);

            //инфраструктурные сервисы
            _dependencyContainer.Add<SignalBusService>();
            _dependencyContainer.Add<GameStateService>();
            _dependencyContainer.Bind<GameSaveDataService>();
            _dependencyContainer.Bind<ViewService>();

            //модели и сопутствующие классы
            _dependencyContainer.Add<CharacterInfo>();
            _dependencyContainer.Add<PlayerLevel>();
            _dependencyContainer.Add<UserInfo>();
            _dependencyContainer.Bind<CharacterSaveLoadHandler>();
            _dependencyContainer.Bind<PlayerLevelSaveLoadHandler>();
            _dependencyContainer.Bind<UserInfoSaveLoadHandler>();

            //PM
            _dependencyContainer.Bind<UserInfoPresentationModel>();
            _dependencyContainer.Bind<LevelUpPresentationModel>();
            //инфраструктурные контроллеры\правила
            _dependencyContainer.Bind<GameSaveLoadRule>();

            //непосредственно игровая логика
            _dependencyContainer.Bind<OpenLevelUpPopupController>();
            _dependencyContainer.Bind<OpenUserInfoPopupController>();
            _dependencyContainer.Bind<AddDebugExperienceController>();
            _dependencyContainer.Bind<AddDebugStatsController>();
            _dependencyContainer.Bind<LevelUpController>();

            //ищем по сцене кандидатов на инъекцию .Inject()
            SearchAndInject();
        }

        private void SearchAndInject()
        {
            var objects = FindObjectsOfType<MonoBehaviour>();
            foreach (var obj in objects)
            {
                var monoBehaviour = obj.GetComponent<MonoBehaviour>();
                if(monoBehaviour != null)
                    _dependencyContainer.Inject(monoBehaviour);
            }
        }

        private void Initialize()
        {
            var inits = _dependencyContainer.GetList<IInit>();
            foreach (var init in inits)
            {
                init.Init();
            }
        }
    }
}