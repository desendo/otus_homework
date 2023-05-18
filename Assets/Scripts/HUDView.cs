using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _gameFinishedContainer;
    [SerializeField] private GameObject _gamePausedContainer;
    [SerializeField] private TextMeshProUGUI _counter;

    private GameStateManager _gameStateManager;

    public void Initialize(GameStateManager gameStateManager, GameStartController gameStartController)
    {
        _gameFinishedContainer.gameObject.SetActive(false);

        _gameStateManager = gameStateManager;
        _gameStateManager.OnGameEnd += OnGameEnd;
        _gameStateManager.OnGameStarted += OnGameStarted;
        _gameStateManager.OnGameIsPaused += OnGamePaused;
        gameStartController.OnCountDownStarted += OnCountDownStarted;
        gameStartController.OnCountDownLeft += OnCountDownLeft;

        _pauseButton.onClick.AddListener(_gameStateManager.TogglePause);
        _startGameButton.onClick.AddListener(gameStartController.StartCountDown);
    }

    private void OnCountDownLeft(float obj)
    {
        _counter.text = Mathf.CeilToInt(obj).ToString();

    }

    private void OnCountDownStarted()
    {
        _startGameButton.gameObject.SetActive(false);
    }


    private void OnGameStarted()
    {
        _counter.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    private void OnGamePaused(bool obj)
    {
        _gamePausedContainer.SetActive(obj);
    }

    private void OnGameEnd()
    {
        _gameFinishedContainer.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }
}