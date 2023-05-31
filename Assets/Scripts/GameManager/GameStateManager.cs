using System.Collections.Generic;

namespace GameManager
{
    public sealed class GameStateManager
    {
        private readonly List<IStartGame> _startGameListeners;
        private readonly List<IFinishGame> _finishGameListeners;

        public GameStateManager(List<IStartGame> startGameListeners, List<IFinishGame> finishGameListeners,
            GameStateService gameStateService)
        {
            _startGameListeners = startGameListeners;
            _finishGameListeners = finishGameListeners;
            gameStateService.OnGameStart.Subscribe(OnGameStart);
            gameStateService.OnGameFinish.Subscribe(OnGameFinish);
        }

        private void OnGameFinish()
        {
            foreach (var finishGame in _finishGameListeners)
                finishGame.FinishGame();
        }

        private void OnGameStart()
        {
            foreach (var startGame in _startGameListeners)
                startGame.StartGame();
        }
    }
}