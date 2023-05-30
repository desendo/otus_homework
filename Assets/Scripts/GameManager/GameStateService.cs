using System;
using System.Collections.Generic;
using ReactiveExtension;

namespace GameManager
{
    public sealed class GameStateService
    {
        private readonly List<IStartGame> _startGames;
        private readonly List<IFinishGame> _finishGames;
        private readonly List<IReset> _resets;
        public readonly Reactive<bool> GameStartedReactive = new Reactive<bool>();
        public event Action OnGameStart;
        public event Action OnGameFinish;


        public void SetGameStarted()
        {
            GameStartedReactive.Value = true;
            OnGameStart?.Invoke();
        }
        public void SetGameFinished()
        {
            GameStartedReactive.Value = false;
            OnGameFinish?.Invoke();
        }
    }
}