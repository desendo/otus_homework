using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _fillComplete;
        [SerializeField] private Image _progress;
        [SerializeField] private TMP_Text _text;
        private TweenerCore<float, float, FloatOptions> _tween;

        public void SetFill(float fill)
        {
            _tween?.Kill();
            _tween = _progress
                .DOFillAmount(fill, 0.4f)
                .OnKill(()=>_progress.fillAmount = fill);

        }
        public void SetText(string text)
        {
            _text.text = text;
        }
        public void SetFillComplete(bool isComplete)
        {
            _fillComplete.SetActive(isComplete);
        }
    }
}