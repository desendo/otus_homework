using System.Collections.Generic;
using Config;
using DependencyInjection;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private List<ChestTimer> _timers;

    private DependencyContainer _container;
    private List<IUpdate> _updates = new List<IUpdate>();

    private void Start()
    {
        Bind();
        Initialize();
    }

    private void Initialize()
    {
        _updates = _container.GetList<IUpdate>();
    }


    private void Bind()
    {
        //infrastructure
        _container = new DependencyContainer();
        _container.Add(_container);
        _container.Add(_visualConfig);
        _container.Add(_gameConfig);
        foreach (var chestTimer in _timers)
            _container.Add(chestTimer);

        _container.Bind<InGameTimeProvider>();
        _container.Bind<ChestTimerInitializer>();
        _container.Bind<GiveChestRewardController>();
        _container.Bind<Saver>();
        SearchAndInject();

    }

    private void Update()
    {
        foreach (var update in _updates)
        {
            update.Update(Time.deltaTime);
        }
    }

    private void SearchAndInject()
    {
        var objects = FindObjectsOfType<MonoBehaviour>(true);
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