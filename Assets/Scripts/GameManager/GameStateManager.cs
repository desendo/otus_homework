using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public sealed class GameStateManager
    {
        private readonly List<IStartGame> _startGames;
        private readonly List<IFinishGame> _finishGames;
        private readonly List<IReset> _resets;

        public GameStateManager(List<IStartGame> startGames, List<IFinishGame> finishGames, List<IReset> resets, GameStateService gameStateService)
        {
            _startGames = startGames;
            _finishGames = finishGames;
            _resets = resets;
            gameStateService.OnGameStart += OnGameStart;
            gameStateService.OnGameFinish += OnGameFinish;
        }

        private void OnGameFinish()
        {
            Time.timeScale = 0f;
            foreach (var finishGame in _finishGames)
                finishGame.FinishGame();
        }

        private void OnGameStart()
        {
            Time.timeScale = 1f;
            foreach (var reset in _resets)
                reset.DoReset();
            foreach (var startGame in _startGames)
                startGame.StartGame();
        }
    }
}