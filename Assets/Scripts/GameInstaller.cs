using Bullets;
using Character;
using Config;
using DependencyInjection;
using Enemy;
using GameManager;
using Input;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private UpdateManager _updateManager;
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private CharacterMono _character;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private BulletSystem _bulletSystem;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private CanvasView _canvasView;

    private DependencyContainer _container;
    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _container = new DependencyContainer();
        _container.AddOnly(_gameConfig);

        _container.AddOnly(_enemyPositions);
        _container.AddInject(_enemyPool);
        _container.AddOnly(_character);
        _container.AddOnly(_bulletSystem);

        _container.AddInject<GameStateService>();
        _container.AddInject<InputManager>();

        _container.AddInject<EnemyManager>();
        _container.AddInject<EnemyDeathController>();
        _container.AddInject<EnemySetFireTargetController>();
        _container.AddInject<EnemyFireController>();
        _container.AddInject<EnemySetMoveTargetController>();
        _container.AddInject<EnemySetBalanceParametersController>();

        _container.AddInject<CharacterDeathController>();
        _container.AddInject<CharacterFireController>();
        _container.AddInject<CharacterMoveController>();
        _container.AddInject<CharacterSetParametersController>();

        _container.AddInject<GameStateManager>();
        _container.AddInject(_canvasView);
        _container.InjectOnly(_updateManager);

    }
}