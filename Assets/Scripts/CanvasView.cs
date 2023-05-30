using Character;
using Components;
using DependencyInjection;
using Enemy;
using GameManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IDisposable = ReactiveExtension.IDisposable;

public class CanvasView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _life;
    private EnemyManager _enemyManager;
    private CharacterMono _character;
    private GameStateService _gameStateService;
    private IDisposable _killedSub;
    private IDisposable _gameStartSub;
    private IDisposable _hpLeftSubSub;

    [Inject]
    public void Construct(CharacterMono character, GameStateService gameStateService, EnemyManager enemyManager)
    {
        _character = character;
        _enemyManager = enemyManager;
        _gameStateService = gameStateService;
        _startButton.onClick.AddListener(gameStateService.SetGameStarted);

        _gameStartSub = _gameStateService.GameStartedReactive.Subscribe(isGameStarted =>
        {
            if (isGameStarted)
            {
                _killedSub = _enemyManager.OnEnemiesKilledReactive.Subscribe(EnemyManagerOnOnEnemiesKilled);
                _hpLeftSubSub = _character.GetComponent<HitPointsComponent>().HpLeft
                    .Subscribe(OnHitPointsComponentOnHpLeft);
                _startButton.gameObject.SetActive(false);
            }
            else
            {
                _killedSub?.Dispose();
                _hpLeftSubSub?.Dispose();
                _startButton.gameObject.SetActive(true);
            }
        });

    }

    private void EnemyManagerOnOnEnemiesKilled(int obj)
    {
        _score.text = "SCORE:" + obj;
    }

    private void OnHitPointsComponentOnHpLeft(int x)
    {
        _life.text = "HP:" + x;
    }

    private void OnDestroy()
    {
        _gameStartSub?.Dispose();
        _killedSub?.Dispose();
        _hpLeftSubSub?.Dispose();
    }
}