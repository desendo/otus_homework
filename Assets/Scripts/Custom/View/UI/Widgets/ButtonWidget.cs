using System;
using Custom.View.PresentationModel;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    [RequireComponent(typeof(Button))]
    public class ButtonWidget : MonoBehaviour
    {
        [SerializeField] private Button _button;
        public event Action OnClickEvent;

        public Button Button => _button;
        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClickEvent?.Invoke());
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}