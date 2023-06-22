using Config;
using Controllers;
using DependencyInjection;
using GameManager;
using Input;
using Services;
using State;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private UpdateManager _updateManager;
    private DependencyContainer _container;

    private void Start()
    {
        Bind();
        _container.Get<GameStateManager>().State.Value = LevelState.None;
        var spawners = _container.GetList<ISpawner>();
        foreach (var spawner in spawners)
        {
            spawner.Spawn();
        }
        _container.Get<GameStateManager>().GameLoaded.Value = true;
        _container.Get<GameStateManager>().State.Value = LevelState.Started;

    }

    private void Bind()
    {
        _container = new DependencyContainer();
        _container.Add(_container);
        _container.Add(_visualConfig);
        _container.Add(_gameConfig);

        _container.Add<UpdateProvider>();

        _container.Bind<InputService>();
        _container.Bind<HeroService>();


        //controllers
        _container.Bind<MoveHeroController>();
        _container.Bind<HeroMoveEngine>();
        _container.Bind<CameraFollowController>();
        _container.Bind<RotateHeroController>();

        _container.Bind<GameStateManager>();
        _container.Inject(_updateManager);

    }
}