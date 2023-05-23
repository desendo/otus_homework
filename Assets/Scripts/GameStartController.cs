using System;

public class GameStartController : ITick, IGameStartListener, IGameReadyListener, IGameFinishListener
{
    private bool _gameStarted;
    public event Action OnCountDownStarted;
    public event Action<float> OnCountDownLeft;

    private readonly float _seconds = 3f;
    private float _timer;
    private readonly GameStateService _gameStateService;
    private bool _countDownStarted;

    public GameStartController(GameStateService gameStateService)
    {
        _gameStateService = gameStateService;
    }

    public void StartCountDown()
    {
        if(_gameStarted)
            return;

        _countDownStarted = true;
        OnCountDownStarted?.Invoke();
    }
    public void Tick(float dt)
    {
        if(_gameStarted || !_countDownStarted)
            return;

        _timer -= dt;
        OnCountDownLeft?.Invoke(_timer);
        if (_timer <= 0f)
        {
            _countDownStarted = false;
            _gameStateService.SetGameStarted();
        }
    }

    public void OnGameStart()
    {
        _gameStarted = true;
    }

    public void OnGameReady()
    {
        _countDownStarted = false;
        _gameStarted = false;
        _timer = _seconds;
    }

    public void OnGameFinish()
    {
        _gameStarted = false;
    }
}