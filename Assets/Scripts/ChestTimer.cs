using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestTimer : MonoBehaviour
{
    [SerializeField] private Button _getButton;
    [SerializeField] private Image _progress;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _chestId;

    private float _timeLeft;
    private float _max;

    private bool _rewardReady;
    private bool _isSetUp;

    public event Action OnChange;
    public event Action<ChestTimer> OnClick;
    public string ChestId => _chestId;
    public bool RewardReady => _rewardReady;
    public float Normalized => _timeLeft / _max;
    public float TimeLeft => _timeLeft;

    public bool IsSetUp => _isSetUp;

    private void Awake()
    {
        OnChange += UpdateView;
        _getButton.onClick.AddListener(() => OnClick?.Invoke(this));
    }


    private void UpdateView()
    {
        _getButton.interactable = _rewardReady;
        _progress.fillAmount =  (1f - Normalized);
        _text.text = TimeSpan.FromSeconds(_timeLeft)
            .ToString("g", CultureInfo.InvariantCulture)
            .Split('.')[0];
    }

    public void SetupTimer(float max, float left)
    {
        _isSetUp = true;
        _max = max;
        _timeLeft = left;
        OnChange?.Invoke();

    }

    public void SetupIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }

    public void AddTime(float time)
    {
        if(_rewardReady)
            return;

        _timeLeft -= time;
        UpdateView();
        if (_timeLeft <= 0f)
        {
            _timeLeft = 0f;
            _rewardReady = true;
            OnChange?.Invoke();
        }
    }

    public void ResetTimer()
    {
        _timeLeft = _max;
        _rewardReady = false;
        OnChange?.Invoke();
    }
}
