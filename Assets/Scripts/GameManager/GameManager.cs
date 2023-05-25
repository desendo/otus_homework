using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public sealed class GameStateManager
    {
        public GameStateManager(List<IStartGame> startGames, List<IFinishGame> finishGames, GameStateService gameStateService)
        {
            gameStateService.OnGameStart += () =>
            {
                Time.timeScale = 1f;
                startGames.ForEach(x => x.StartGame());
            };
            gameStateService.OnGameFinish += () =>
            {
                Time.timeScale = 0f;
                finishGames.ForEach(x => x.FinishGame());
            };
        }

    }
}