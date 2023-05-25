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


    [Inject]
    public void Construct(Character.Character character, GameStateService gameStateManager, EnemyManager enemyManager)
    {
        _startButton.onClick.AddListener(gameStateManager.SetGameStarted);
        gameStateManager.OnGameStart += () =>
        {
            enemyManager.OnEnemiesKilled += EnemyManagerOnOnEnemiesKilled;
            character.HitPointsComponent.HpLeft += OnHitPointsComponentOnHpLeft;
            OnHitPointsComponentOnHpLeft(character.HitPointsComponent.HitPoints);
            EnemyManagerOnOnEnemiesKilled(enemyManager.KilledEnemies);
            _startButton.gameObject.SetActive(false);
        };
        gameStateManager.OnGameFinish += () =>
        {
            enemyManager.OnEnemiesKilled -= EnemyManagerOnOnEnemiesKilled;

            character.HitPointsComponent.HpLeft -= OnHitPointsComponentOnHpLeft;
            _startButton.gameObject.SetActive(true);
        };
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