
using Config;
using DependencyInjection;

using GameManager;

using UnityEngine;

public class GameInstaller : MonoBehaviour
{

    private DependencyContainer _container;
    private void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _container = new DependencyContainer();
    }
}