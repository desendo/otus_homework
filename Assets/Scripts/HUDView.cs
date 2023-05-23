using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _restartGameButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _gameFinishedContainer;
    [SerializeField] private GameObject _gamePausedContainer;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _passed;

    private GameStateService _gameStateService;
    private GameStartController _gameStartController;
    private PlayerController _playerController;
    private WorldManager _worldManager;

    public void Initialize(GameStateService gameStateService, GameStartController gameStartController,
        PlayerController playerController, WorldManager worldManager)
    {
        _gameFinishedContainer.gameObject.SetActive(false);
        _gameStartController = gameStartController;
        _playerController = playerController;
        _gameStateService = gameStateService;
        _worldManager = worldManager;

        _gameStateService.OnGameFinished += OnGameFinished;
        _gameStateService.OnGameStarted += OnGameStarted;
        _gameStateService.OnGameIsPaused += OnGamePaused;
        _gameStateService.OnGameReady += OnGameReady;

        _gameStartController.OnCountDownStarted += OnCountDownStarted;
        _gameStartController.OnCountDownLeft += OnCountDownLeft;

        _playerController.OnScore += OnScore;

        _worldManager.OnDistance += OnDistance;

        _pauseButton.onClick.AddListener(_gameStateService.TogglePause);
        _startGameButton.onClick.AddListener(gameStartController.StartCountDown);
        _restartGameButton.onClick.AddListener(()=>
        {
            _gameStateService.SetGameReady();
            _gameStartController.StartCountDown();
        });
    }

    private void OnDestroy()
    {
        _gameStateService.OnGameFinished -= OnGameFinished;
        _gameStateService.OnGameStarted -= OnGameStarted;
        _gameStateService.OnGameIsPaused -= OnGamePaused;
        _gameStateService.OnGameReady -= OnGameReady;

        _gameStartController.OnCountDownStarted -= OnCountDownStarted;
        _gameStartController.OnCountDownLeft -= OnCountDownLeft;

        _playerController.OnScore -= OnScore;

        _worldManager.OnDistance -= OnDistance;

    }

    private void OnDistance(float obj)
    {
        _passed.text = $"Passed: {obj:0}m";
    }

    private void OnScore(int obj)
    {
        _score.text = $"Score: {obj}";
    }

    private void OnGameReady()
    {
        _gameFinishedContainer.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _startGameButton.gameObject.SetActive(true);
        _restartGameButton.gameObject.SetActive(false);
    }

    private void OnCountDownLeft(float obj)
    {
        _counter.text = Mathf.CeilToInt(obj).ToString();

    }

    private void OnCountDownStarted()
    {
        _counter.gameObject.SetActive(true);
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

    private void OnGameFinished()
    {
        _gameFinishedContainer.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _restartGameButton.gameObject.SetActive(true);
    }
}