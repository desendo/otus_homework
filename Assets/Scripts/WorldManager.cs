using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class WorldManager: IGameStartListener, IGameFinishListener, IPauseListener, ITick, IGameReadyListener
{

    public event Action<float> OnDistance;
    private readonly List<Transform> _spawnPoints;
    private readonly int _pointCounts;
    private readonly float _defaultSpeed;
    private readonly float _defaultDelay;

    private readonly WorldObjectBase[] _prefabs;

    private readonly List<WorldObjectBase> _spawnedObjects = new List<WorldObjectBase>();

    private bool _isPaused;
    private bool _gameStarted;
    private float _timeToSpawnLeft;
    private readonly ObjectSpawner _spawner;
    private float _spawnDelay;
    private float _moveSpeed;
    private float _distancePassed;

    public WorldManager(List<Transform> spawnPoints, float spawnDelay, WorldObjectBase[] prefabs, float moveSpeed)
    {
        _spawnPoints = spawnPoints;
        _pointCounts = spawnPoints.Count;
        _defaultDelay = spawnDelay;
        _defaultSpeed = moveSpeed;

        _spawnDelay = _defaultDelay;
        _moveSpeed = _defaultSpeed;
        _prefabs = prefabs;
        _spawner = new ObjectSpawner(_spawnedObjects);
    }

    public void SetMoveFactor(float speedFactor)
    {
        _moveSpeed = _defaultSpeed * speedFactor;
        _spawnDelay = _defaultDelay / speedFactor;
    }

    public void OnGameStart()
    {
        _gameStarted = true;
        _timeToSpawnLeft = 0f;
    }

    public void OnGameFinish()
    {
        _gameStarted = false;
        _timeToSpawnLeft = _spawnDelay;
    }

    public void Tick(float dt)
    {
        if(!_gameStarted || _isPaused)
            return;

        _distancePassed += _moveSpeed * dt;
        OnDistance?.Invoke(_distancePassed);

        UpdateObjects(dt);
        TryToSpawn(dt);
    }

    private void UpdateObjects(float dt)
    {
        for (var i = 0; i < _spawnedObjects.Count; i++)
        {
            _spawnedObjects[i].DoUpdate(dt);
        }
    }

    private void TryToSpawn(float dt)
    {
        _timeToSpawnLeft -= dt;

        if (_timeToSpawnLeft > 0f)
            return;

        _timeToSpawnLeft = _spawnDelay;
        var point = GetRandomSpawnPoint();
        var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
        var obj = _spawner.Spawn(prefab, point.position);
        obj.Initialize(_moveSpeed * -point.forward);
    }

    private Transform GetRandomSpawnPoint()
    {
        var index = Random.Range(0, _pointCounts);
        return _spawnPoints[index];
    }

    public void OnPaused(bool isPaused)
    {
        _isPaused = isPaused;
        foreach (var worldObject in _spawnedObjects)
            worldObject.enabled = !isPaused;
    }

    public void OnGameReady()
    {
        foreach (var obj in _spawnedObjects)
        {
            Object.Destroy(obj.gameObject);
        }
        _spawnedObjects.Clear();

        _distancePassed = 0;
        OnDistance?.Invoke(_distancePassed);
    }
}
