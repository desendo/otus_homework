using Config;
using DependencyInjection;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;

    private DependencyContainer _container;

    private void Start()
    {
        Bind();
    }


    private void Bind()
    {
        //infrastructure
        _container = new DependencyContainer();
        _container.Add(_container);
        _container.Add(_visualConfig);
        _container.Add(_gameConfig);

        SearchAndInject();

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