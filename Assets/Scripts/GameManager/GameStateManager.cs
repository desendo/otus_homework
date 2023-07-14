using System.Collections.Generic;
using Common.Atomic.Values;
using Signals;

namespace GameManager {

    public class GameStateManager
    {
        private readonly List<IGameLoadedListener> _gameLoadListeners;
        private readonly List<IFinishGameListener> _finishGameListeners;

        private readonly List<IStartGameListener> _startListeners;
        public readonly AtomicVariable<LevelState> State = new AtomicVariable<LevelState>(LevelState.None);
        public readonly AtomicVariable<bool> GameLoaded = new AtomicVariable<bool>();
        private readonly List<ISpawner> _spawners;

        public GameStateManager(List<IGameLoadedListener> gameLoadListeners,
            List<IStartGameListener> startListeners,
            List<IFinishGameListener> finishGameListeners,
            List<ISpawner> spawners,
            SignalBusService signalBusService)
        {
            _gameLoadListeners = gameLoadListeners;
            _finishGameListeners = finishGameListeners;
            _startListeners = startListeners;
            _spawners = spawners;

            signalBusService.Subscribe<GameOverRequest>(HandleGameOverRequest);
            signalBusService.Subscribe<GameStartRequest>(HandleGameStartRequest);
            signalBusService.Subscribe<GameWinRequest>(HandleGameWinRequest);

            GameLoaded.OnChanged.Subscribe(GameLoadedChanged);
            State.OnChanged.Subscribe(GameStateChanged);
        }

        private void HandleGameOverRequest(GameOverRequest obj)
        {
            State.Value = LevelState.Lost;
        }
        private void HandleGameStartRequest(GameStartRequest obj)
        {
            GameLoaded.Value = false;
            foreach (var spawner in _spawners)
            {
                spawner.Clear();
                spawner.Spawn();
            }
            GameLoaded.Value = true;
            State.Value = LevelState.Started;
        }
        private void HandleGameWinRequest(GameWinRequest obj)
        {
            State.Value = LevelState.Win;
        }


        private void GameStateChanged(LevelState obj)
        {
            if (obj == LevelState.Lost)
                foreach (var listener in _finishGameListeners)
                    listener.OnFinishGame(false);

            else if (obj == LevelState.Win)
                foreach (var listener in _finishGameListeners)
                    listener.OnFinishGame(true);

            else if (obj == LevelState.Started)
                foreach (var listener in _startListeners)
                    listener.OnStartGame();
        }

        private void GameLoadedChanged(bool x)
        {
            foreach (var listener in _gameLoadListeners)
            {
                listener.OnGameLoaded(x);
            }
        }
    }

    public enum LevelState
    {
        None = 0,
        Started = 1,
        Lost = 2,
        Win = 3
    }
}