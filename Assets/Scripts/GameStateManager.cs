using System;

public class GameStateManager
{

    public event Action OnGameStarted;
    public event Action OnGameEnd;
    public event Action<bool> OnGameIsPaused;

    private bool _isPaused = false;
    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }
    public void FinishGame()
    {
        OnGameEnd?.Invoke();
    }
    public void SetGamePaused(bool isPaused)
    {
        OnGameIsPaused?.Invoke(isPaused);
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        OnGameIsPaused?.Invoke(_isPaused);
    }
}