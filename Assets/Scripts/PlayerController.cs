using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : IGameStartListener, IGameFinishListener, IPauseListener, ITick, IGameReadyListener
{
    public event Action<Vector3> OnLaneChanged;
    public event Action<int> OnScore;
    public event Action OnHit;

    public int Score => _score;
    private readonly List<Transform> _positionAnchors;
    private int _currentAnchorIndex;
    private bool _gameStarted;
    private bool _gamePaused;
    private int _score;
    private readonly float _playerChangeLaneSpeed;

    public PlayerController(List<Transform> positionAnchors, float playerChangeLaneSpeed)
    {
        _positionAnchors = positionAnchors;
        _playerChangeLaneSpeed = playerChangeLaneSpeed;
    }
    public void OnGameReady()
    {
        _score = 0;
        OnScore?.Invoke(_score);
        _currentAnchorIndex = 1;
        OnLaneChanged?.Invoke(_positionAnchors[_currentAnchorIndex].position);
    }

    public void OnGameStart()
    {
        _gameStarted = true;

    }

    public void OnGameFinish()
    {
        _gameStarted = false;
    }

    public void OnPaused(bool isPaused)
    {
        _gamePaused = isPaused;
    }

    public void Tick(float dt)
    {
        if(_gamePaused || !_gameStarted)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(_currentAnchorIndex == 0)
                return;

            _currentAnchorIndex--;
            OnLaneChanged?.Invoke(_positionAnchors[_currentAnchorIndex].position);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(_currentAnchorIndex == _positionAnchors.Count - 1)
                return;

            _currentAnchorIndex++;
            OnLaneChanged?.Invoke(_positionAnchors[_currentAnchorIndex].position);
        }
    }


    public void PlayerCollision(WorldObjectBase worldObjectBase)
    {
        if (worldObjectBase != null)
        {
            if(worldObjectBase.Type == WorldObjectBase.ObjType.Enemy)
                OnHit?.Invoke();
            else if (worldObjectBase.Type == WorldObjectBase.ObjType.Coin)
            {
                _score++;
                OnScore?.Invoke(_score);
            }
        }

    }

    public float GetLaneChangeSpeed()
    {
        return _playerChangeLaneSpeed;
    }


}