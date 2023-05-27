using System;
using System.Collections.Generic;

namespace GameManager
{
    public sealed class GameStateService
    {
        private readonly List<IStartGame> _startGames;
        private readonly List<IFinishGame> _finishGames;
        private readonly List<IReset> _resets;
        public event Action OnGameStart;
        public event Action OnGameFinish;

        public bool GameStarted { get; private set; }

        public void SetGameStarted()
        {
            GameStarted = true;
            OnGameStart?.Invoke();
        }
        public void SetGameFinished()
        {
            GameStarted = false;
            OnGameFinish?.Invoke();
        }
    }
}