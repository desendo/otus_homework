using Bullets;
using Character;
using Config;
using DependencyInjection;
using Effects;
using Enemy;
using GameManager;
using Input;
using Level;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private UpdateManager _updateManager;
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private ExplosionEffectPool _explosionEffectPool;
    [SerializeField] private HitEffectPool _hitEffectPool;
    [SerializeField] private CanvasView _canvasView;

    private DependencyContainer _container;
    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _container = new DependencyContainer();
        _container.Add(_gameConfig);

        _container.Add(_enemyPool);
        _container.Add(_bulletPool);
        _container.Add(_hitEffectPool);
        _container.Add(_explosionEffectPool);

        _container.Add(_levelBounds);
        _container.Add(_enemyPositions);

        _container.Add<GameStateService>();
        _container.Add<InputManager>();
        _container.Add<CharacterService>().SetCharacter(_character);

        _container.Bind<EffectsService>();

        _container.Bind<BulletSpawner>();
        _container.Bind<BulletManager>();

        _container.Bind<EnemySpawner>();
        _container.Bind<EnemyManager>();
        _container.Bind<EnemyDeathController>();
        _container.Bind<EnemyFireController>();

        _container.Bind<CharacterDeathObserver>();
        _container.Bind<CharacterFireController>();
        _container.Bind<CharacterMoveController>();
        _container.Bind<CharacterInstaller>();

        _container.Bind<GameStateManager>();
        _container.Bind(_canvasView);
        _container.Inject(_updateManager);
    }
}