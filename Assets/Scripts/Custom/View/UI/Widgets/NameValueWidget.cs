using System;
using Custom.View.PresentationModel;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Custom.View.UI.Widgets
{
    public class NameValueWidget : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private CanvasGroup _blink;
        private INameValuePresentationModel _pm;
        private Sequence _seq;

        public void Bind(INameValuePresentationModel pm)
        {
            _pm = pm;
            _pm.OnValueChanged += OnChanged;
            OnChanged(_pm.GetValue());
        }

        private void OnChanged(int obj)
        {
            _title.text = $"{_pm.GetName()}:{_pm.GetValue()}";
            _seq?.Kill();
            _seq = DOTween.Sequence();
            _seq
                .Append(_blink.DOFade(1, 0.1f))
                .Append(_blink.DOFade(0, 0.3f));
        }

        public void Dispose()
        {
            _seq?.Kill();
            _pm.OnValueChanged -= OnChanged;
        }
    }
}