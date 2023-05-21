using System;

namespace ShootEmUp
{
    public class GameStateService
    {
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