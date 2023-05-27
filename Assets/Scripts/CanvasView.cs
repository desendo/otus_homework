using System;
using Character;
using Components;
using DependencyInjection;
using Enemy;
using GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _life;
    private EnemyManager _enemyManager;
    private CharacterMono _character;

    [Inject]
    public void Construct(CharacterMono character, GameStateService gameStateManager, EnemyManager enemyManager)
    {
        _character = character;
        _enemyManager = enemyManager;
        _startButton.onClick.AddListener(gameStateManager.SetGameStarted);
        gameStateManager.OnGameStart += () =>
        {
            _enemyManager.OnEnemiesKilled += EnemyManagerOnOnEnemiesKilled;
            _character.GetComponent<HitPointsComponent>().HpLeft += OnHitPointsComponentOnHpLeft;
            OnHitPointsComponentOnHpLeft(_character.GetComponent<HitPointsComponent>().HitPoints);
            EnemyManagerOnOnEnemiesKilled(_enemyManager.KilledEnemies);
            _startButton.gameObject.SetActive(false);
        };
        gameStateManager.OnGameFinish += () =>
        {
            _enemyManager.OnEnemiesKilled -= EnemyManagerOnOnEnemiesKilled;

            _character.GetComponent<HitPointsComponent>().HpLeft -= OnHitPointsComponentOnHpLeft;
            _startButton.gameObject.SetActive(true);
        };
    }

    private void OnEnable()
    {
        OnHitPointsComponentOnHpLeft(_character.GetComponent<HitPointsComponent>().HitPoints);
        EnemyManagerOnOnEnemiesKilled(_enemyManager.KilledEnemies);
    }

    private void EnemyManagerOnOnEnemiesKilled(int obj)
    {
        _score.text = "SCORE:" + obj;
    }

    private void OnHitPointsComponentOnHpLeft(int x)
    {
        _life.text = "HP:" + x;
    }
}