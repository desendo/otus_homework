using System.Collections.Generic;
using Common.Atomic.Values;

namespace State
{

    public class GameStateManager
    {
        private readonly List<IGameLoadedListener> _gameLoadListeners;
        private readonly List<IWinGameListener> _winListeners;
        private readonly List<ILostGameListener> _lostListeners;
        private readonly List<IStartGameListener> _startListeners;
        public readonly AtomicVariable<LevelState> State = new AtomicVariable<LevelState>();
        public readonly AtomicVariable<bool> GameLoaded = new AtomicVariable<bool>();

        public GameStateManager(List<IGameLoadedListener> gameLoadListeners,
            List<IWinGameListener> winListeners,
            List<ILostGameListener> lostListeners,
            List<IStartGameListener> startListeners)
        {
            _gameLoadListeners = gameLoadListeners;
            _winListeners = winListeners;
            _lostListeners = lostListeners;
            _startListeners = startListeners;

            GameLoaded.OnChanged += GameLoadedChanged;
            State.OnChanged += GameStateChanged;
        }

        public void SetLoaded(bool isLoaded)
        {
            GameLoaded.Value = isLoaded;
        }

        private void GameStateChanged(LevelState obj)
        {
            if (obj == LevelState.Lost)
                foreach (var listener in _lostListeners)
                    listener.OnLostGame();

            else if (obj == LevelState.Win)
                foreach (var listener in _winListeners)
                    listener.OnWinGame();

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