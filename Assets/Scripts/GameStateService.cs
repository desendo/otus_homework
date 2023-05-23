using System;

public class GameStateService
{
    public event Action OnGameStarted;
    public event Action OnGameFinished;
    public event Action OnGameReady;
    public event Action<bool> OnGameIsPaused;

    private bool _isPaused = false;

    public void SetGameStarted()
    {
        OnGameStarted?.Invoke();
    }
    public void SetGameReady()
    {
        OnGameReady?.Invoke();
    }
    public void FinishGame()
    {
        OnGameFinished?.Invoke();
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        OnGameIsPaused?.Invoke(_isPaused);
    }
}