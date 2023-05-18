using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: IGameStartListener, IGameEndListener, IPauseListener, ITick
{
    private readonly List<Transform> _enemySpawnPoints;
    private readonly int _pointCounts;
    private readonly float _enemySpawnDelay;
    private readonly float _enemyMoveSpeed;
    private readonly Enemy _prefab;
    private readonly List<Enemy> _spawnedEnemies = new List<Enemy>();

    private bool _isPaused;
    private bool _gameStarted;
    private float _timeToSpawnLeft;

    public EnemyManager(List<Transform> enemySpawnPoints, float enemySpawnDelay, float enemyMoveSpeed, Enemy prefab)
    {
        _enemySpawnPoints = enemySpawnPoints;
        _pointCounts = enemySpawnPoints.Count;
        _enemySpawnDelay = enemySpawnDelay;
        _enemyMoveSpeed = enemyMoveSpeed;
        _prefab = prefab;
    }

    public void OnGameStart()
    {
        _gameStarted = true;
        _timeToSpawnLeft = 0f;
    }

    public void OnGameEnd()
    {
        _gameStarted = false;
        _timeToSpawnLeft = _enemySpawnDelay;
    }

    public void Tick(float dt)
    {
        if(!_gameStarted || _isPaused)
            return;

        MoveEnemies(dt);
        TryToSpawn(dt);
    }

    private void MoveEnemies(float dt)
    {
        _spawnedEnemies.ForEach(x=>x.Move(dt));
    }

    private void TryToSpawn(float dt)
    {
        _timeToSpawnLeft -= dt;

        if (_timeToSpawnLeft > 0f)
            return;

        _timeToSpawnLeft = _enemySpawnDelay;
        var point = GetRandomSpawnPoint();
        SpawnEnemy(point, _enemyMoveSpeed);
    }

    private void SpawnEnemy(Transform point, float enemyMoveSpeed)
    {
        var enemy = Object.Instantiate(_prefab, point.position, Quaternion.identity);
        enemy.Initialize(enemyMoveSpeed * -point.forward, OnEnemyDestroy);
        _spawnedEnemies.Add(enemy);
    }

    private void OnEnemyDestroy(Enemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }

    private Transform GetRandomSpawnPoint()
    {
        var index = Random.Range(0, _pointCounts);
        return _enemySpawnPoints[index];
    }

    public void OnPaused(bool isPaused)
    {
        _isPaused = isPaused;
        _spawnedEnemies.ForEach(x=>x.enabled = !isPaused);
    }
}
