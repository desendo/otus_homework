using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Transform> _playerPositions;

    [SerializeField] private float _playerChangeLaneSpeed;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private WorldObjectBase[] _prefabs;
    [SerializeField] private Player _player;
    [SerializeField] private HUDView _hudView;

    private readonly List<IGameListener> _gameListeners = new List<IGameListener>();
    private readonly List<ITick> _iTicks = new List<ITick>();
    private GameStateService _gameStateService;

    private void Start()
    {
        _gameStateService = new GameStateService();
        _gameStateService.OnGameFinished += OnGameFinished;
        _gameStateService.OnGameStarted += OnGameStarted;
        _gameStateService.OnGameReady += OnGameReady;
        _gameStateService.OnGameIsPaused += OnGamePause;

        var worldManager = new WorldManager(_spawnPoints, _spawnDelay, _prefabs, _moveSpeed);
        Add(worldManager);
        var gameStartController = new GameStartController(_gameStateService);
        Add(gameStartController);
        var playerController = new PlayerController(_playerPositions, _playerChangeLaneSpeed);
        Add(playerController);

        var gameFinishController = new GameFinishController(_gameStateService, playerController);
        Add(gameFinishController);

        var gameSpeedController = new GameSpeedController(playerController, worldManager);
        Add(gameSpeedController);

        _hudView.Initialize(_gameStateService, gameStartController, playerController, worldManager);
        _player.Initialize(playerController);

        _gameStateService.SetGameReady();
    }
    private void Update()
    {
        for (var i = 0; i < _iTicks.Count; i++)
        {
            var tick = _iTicks[i];
            tick.Tick(Time.deltaTime);
        }
    }
    private void OnGamePause(bool obj)
    {
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IPauseListener pauseListener)
            {
                pauseListener.OnPaused(obj);
            }
        }
    }

    private void OnGameFinished()
    {
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IGameFinishListener gameEndListener)
            {
                gameEndListener.OnGameFinish();
            }
        }
    }
    private void OnGameStarted()
    {
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IGameStartListener gameStartListener)
            {
                gameStartListener.OnGameStart();
            }
        }
    }
    private void OnGameReady()
    {
        foreach (var gameListener in _gameListeners)
        {
            if (gameListener is IGameReadyListener gameReadyListener)
            {
                gameReadyListener.OnGameReady();
            }
        }
    }

    private void Add(object manager)
    {
        if(manager is ITick tick)
            _iTicks.Add(tick);
        if(manager is IGameListener listener)
            _gameListeners.Add(listener);
    }

    private void OnDestroy()
    {
        _gameStateService.OnGameFinished -= OnGameFinished;
        _gameStateService.OnGameStarted -= OnGameStarted;
        _gameStateService.OnGameReady -= OnGameReady;
        _gameStateService.OnGameIsPaused -= OnGamePause;
    }
}