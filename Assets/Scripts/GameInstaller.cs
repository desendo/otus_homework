using DependencyInjection;
using Homework;
using Homework.SceneObjects;
using Homework.Signals;
using Pool;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private PoolViewByIdBase<SceneObjectView> _unitPool;
    [SerializeField] private UnitObjectsHandler _objectsHandler;
    [SerializeField] private ResourceObjectHandler _resHandler;
    [SerializeField] private PlayerResources _playerResources;

    private DependencyContainer _container;
    public void Start()
    {
        Bind();

        _container.Get<SaveLoadController>().Load();
    }

    private void Bind()
    {
        _container = new DependencyContainer();
        _container.Bind<SignalBusService>();

        _container.Bind(new EncryptedBinaryDataHandler());
        _container.Bind<SaveDataProvider>();

        _container.Add(_unitPool);
        _container.Add(_playerResources);
        _container.Bind(_objectsHandler);
        _container.Bind(_resHandler);
        _container.Bind<PlayerResourcesSaveLoader>();

        _container.Bind<SaveLoadController>();

        SearchAndInject();
    }

    private void SearchAndInject()
    {
        var objects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in objects)
        {
            var monoBehaviours = obj.GetComponents<MonoBehaviour>();
            if (monoBehaviours != null)
            {
                foreach (var monoBehaviour in monoBehaviours)
                {
                    _container.Inject(monoBehaviour);
                }
            }
        }
    }
}