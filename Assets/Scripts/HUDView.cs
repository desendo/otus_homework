using System.Collections;
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

    public void DoReset()
    {

    }

    public void Initialize(GameStateManager gameStateManager)
    {
        _gameFinishedContainer.gameObject.SetActive(false);

        _gameStateManager = gameStateManager;
        _gameStateManager.OnGameEnd += OnGameEnd;
        _gameStateManager.OnGameStarted += OnGameStarted;
        _gameStateManager.OnGameIsPaused += OnGamePaused;
        _pauseButton.onClick.RemoveAllListeners();
        _pauseButton.onClick.AddListener(() => {
        {
            _gameStateManager.TogglePause();
        }});
        _startGameButton.onClick.RemoveAllListeners();
        _startGameButton.onClick.AddListener(() => {
        {
            var startCountDown = StartCountDown();
            StartCoroutine(startCountDown);
        }});
    }

    private IEnumerator StartCountDown()
    {
        _startGameButton.gameObject.SetActive(false);
        _counter.text = "3";
        _counter.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        _counter.text = "2";
        yield return new WaitForSeconds(1);

        _counter.text = "1";
        yield return new WaitForSeconds(1);

        _gameStateManager.StartGame();
        yield return null;
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