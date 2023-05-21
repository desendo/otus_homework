using DependencyInjection;
using UnityEngine;

namespace ShootEmUp
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private UpdateManager _updateManager;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Character _character;
        [SerializeField] private BulletConfig _bulletConfig;
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
            _container.AddOnly(_enemyPositions);
            _container.AddOnly(_enemyPool);
            _container.AddOnly(_character);
            _container.AddOnly(_bulletConfig);
            _container.AddOnly(_bulletSystem);

            _container.AddInject<GameStateService>();
            _container.AddInject<CharacterController>();
            _container.AddInject<InputManager>();
            _container.AddInject<EnemyManager>();

            _container.AddInject<GameStateManager>();
            _container.AddInject(_canvasView);
            _container.InjectOnly(_updateManager);
        }
    }

}