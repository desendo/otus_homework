using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Popup
{

    public interface IView
    {
        public void Show(Action closeCommand, object data = null);
        public void Hide();
    }
    public class PopupBase : MonoBehaviour, IView
    {
        [SerializeField] protected Button _closeButton;
        [SerializeField] protected Button _closeBackButton;

        private object _data;
        private Action _closeCommand;

        protected virtual void Awake()
        {
            if(_closeBackButton != null)
                _closeBackButton.onClick.AddListener(TryClose);

            if(_closeButton != null)
                _closeButton.onClick.AddListener(TryClose);
        }

        public virtual void Show(Action closeCommand, object data = null)
        {
            _closeCommand = closeCommand;
            gameObject.SetActive(true);
            OnShow(data);
        }

        protected virtual void OnShow(object data)
        {
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide();

        }
        protected virtual void OnHide()
        {
        }

        protected void TryClose()
        {
            _closeCommand?.Invoke();
        }
    }
}