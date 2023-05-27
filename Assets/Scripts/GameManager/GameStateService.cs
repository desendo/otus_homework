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

        public void SetGameStarted()
        {
            OnGameStart?.Invoke();
        }
        public void SetGameFinished()
        {
            OnGameFinish?.Invoke();
        }
    }
}