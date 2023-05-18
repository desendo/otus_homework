using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemySpawnPoints;
    [SerializeField] private List<Transform> _playerPositions;

    [SerializeField] private float _playerChangeLaneSpeed;
    [SerializeField] private float _enemySpawnDelay;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private HUDView _hudView;

    private readonly List<IGameEndListener> _gameEndListeners = new List<IGameEndListener>();
    private readonly List<IGameStartListener> _gameStartListeners = new List<IGameStartListener>();
    private readonly List<IPauseListener> _gamePauseListeners = new List<IPauseListener>();
    private readonly List<ITick> _iTicks = new List<ITick>();

    private void Start()
    {
        var gameStateManager = new GameStateManager();
        gameStateManager.OnGameStarted += () => _gameStartListeners.ForEach(x => x.OnGameStart());
        gameStateManager.OnGameEnd += () => _gameEndListeners.ForEach(x => x.OnGameEnd());
        gameStateManager.OnGameIsPaused += isPaused => _gamePauseListeners.ForEach(x => x.OnPaused(isPaused));

        Add(new EnemyManager(_enemySpawnPoints, _enemySpawnDelay, _enemyMoveSpeed, _enemyPrefab));

        var gameStartController = new GameStartController(gameStateManager);
        Add(gameStartController);

        var playerController = new PlayerController(_playerPositions, _playerChangeLaneSpeed);
        Add(playerController);

        var gameFinishController = new GameFinishController(gameStateManager, playerController);

        _hudView.Initialize(gameStateManager, gameStartController);
        _player.Initialize(playerController);


    }
    private void Update()
    {
        _iTicks.ForEach(x=>x.Tick(Time.deltaTime));
    }
    private void Add(object manager)
    {
        if(manager is ITick tick)
            _iTicks.Add(tick);
        if(manager is IGameEndListener gameEndListener)
            _gameEndListeners.Add(gameEndListener);
        if(manager is IGameStartListener gameStartListener)
            _gameStartListeners.Add(gameStartListener);
        if(manager is IPauseListener pauseListener)
            _gamePauseListeners.Add(pauseListener);
    }
}