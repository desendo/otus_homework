using System;
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

    private float _left;
    private float _max;

    private bool _rewardReady;
    private bool _isSetUp;

    public event Action OnChange;
    public event Action<ChestTimer> OnClick;
    public string ChestId => _chestId;
    public bool RewardReady => _rewardReady;
    public float Normalized => _left / _max;
    public float Left => _left;

    public bool IsSetUp => _isSetUp;

    private void Awake()
    {
        OnChange += UpdateView;
        _getButton.onClick.AddListener(()=>OnClick?.Invoke(this));
    }


    private void UpdateView()
    {
        _getButton.interactable = _rewardReady;
        _progress.fillAmount = Normalized;
        _text.text = TimeSpan.FromSeconds(_left).ToString("g").Split(',')[0];
    }

    public void SetupTimer(float max, float left)
    {
        _isSetUp = true;
        _max = max;
        _left = left;
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

        _left -= time;
        UpdateView();
        if (_left <= 0f)
        {
            _left = 0f;
            _rewardReady = true;
            OnChange?.Invoke();
        }
    }

    public void ResetTimer()
    {
        _left = _max;
        _rewardReady = false;
        OnChange?.Invoke();
    }
}
