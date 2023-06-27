using System;
using Custom.View.PresentationModel;
using TMPro;
using UnityEngine;

namespace UI.Widgets
{
    public class NameValueWidget : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _title;
        private INameValuePresentationModel _pm;

        public void Bind(INameValuePresentationModel pm)
        {
            _pm = pm;
            _pm.OnValueChanged += OnChanged;
            OnChanged(_pm.GetValue());
        }

        private void OnChanged(int obj)
        {
            _title.text = $"{_pm.GetName()}:{_pm.GetValue()}";

        }

        public void Dispose()
        {
            _pm.OnValueChanged -= OnChanged;
        }
    }
}