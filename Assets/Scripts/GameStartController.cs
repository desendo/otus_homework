using System;

public class GameStartController : ITick, IGameStartListener
{
    private bool _gameStarted;
    public event Action OnCountDownStarted;
    public event Action<float> OnCountDownLeft;

    private readonly float _seconds = 3f;
    private float _timer;
    private readonly GameStateManager _gameStateManager;
    private bool _countDownStarted;

    public GameStartController(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }


    public void StartCountDown()
    {
        _countDownStarted = true;
        OnCountDownStarted?.Invoke();
        _timer = _seconds;
    }
    public void Tick(float dt)
    {
        if(_gameStarted || !_countDownStarted)
            return;

        _timer -= dt;
        OnCountDownLeft?.Invoke(_timer);
        if(_timer <= 0f)
            _gameStateManager.StartGame();
    }

    public void OnGameStart()
    {
        _gameStarted = true;
    }
}