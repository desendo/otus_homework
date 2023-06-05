using System.Collections.Generic;
using Custom.Data;
using Custom.Services;
using Custom.Signals;
using Custom.View;

namespace Custom.GameManager
{
    public sealed class GameSaveLoadRule: IInit
    {
        private readonly List<IDataLoadHandler<GameSaveData>> _gameDataLoadHandlers;
        private readonly GameStateService _gameStateService;
        private readonly IGameSaveDataService _gameSaveDataService;
        private readonly IViewService _viewService;
        private List<IReset> _resetListeners;

        public GameSaveLoadRule(List<IDataLoadHandler<GameSaveData>> gameDataLoadHandlers, List<IReset> resetListeners, SignalBusService signalBusService,
            GameStateService gameStateService, IGameSaveDataService gameSaveDataService, IViewService viewService)
        {
            _gameDataLoadHandlers = gameDataLoadHandlers;
            _viewService = viewService;
            _gameStateService = gameStateService;
            _resetListeners = resetListeners;
            _gameSaveDataService = gameSaveDataService;
            signalBusService.Subscribe<ResetRequest>(x => ResetGame());
            signalBusService.Subscribe<LoadRequest>(x => LoadGame());
            signalBusService.Subscribe<SaveRequest>(x => SaveGame());
        }
        public void Init()
        {
            LoadGame();
        }
        private void SaveGame()
        {
            foreach (var gameDataLoadHandler in _gameDataLoadHandlers)
            {
                gameDataLoadHandler.SaveToData(_gameSaveDataService.Data);
            }
            _gameSaveDataService.SaveData();

        }
        private void LoadGame()
        {
            _gameStateService.SetGameLoaded(false);
            _viewService.HideAll();
            _gameSaveDataService.Load(data =>
            {
                foreach (var gameDataLoadHandler in _gameDataLoadHandlers)
                {
                    gameDataLoadHandler.LoadFromData(data);
                }

                _gameStateService.SetGameLoaded(true);
            });
        }
        private void ResetGame()
        {
            _gameStateService.SetGameLoaded(false);
            foreach (var resetListener in _resetListeners)
            {
                resetListener.Reset();
            }
            _gameSaveDataService.LoadDefault(data =>
            {
                foreach (var gameDataLoadHandler in _gameDataLoadHandlers)
                {
                    gameDataLoadHandler.LoadFromData(data);
                }
                _gameStateService.SetGameLoaded(true);
            });
        }
    }
}