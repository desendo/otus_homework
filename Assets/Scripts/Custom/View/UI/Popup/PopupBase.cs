using System;
using UnityEngine;

namespace Custom.View.UI.Popup
{

    public interface IView
    {
        public void Show(Action closeCommand, object data = null);
        public void Hide();
    }
    public class PopupBase : MonoBehaviour, IView
    {
        private object _data;
        private Action _closeCommand;

        public virtual void Show(Action closeCommand, object data = null)
        {
            _closeCommand = closeCommand;
            gameObject.SetActive(true);
            OnDataSet(data);
            OnShow();
        }
        protected virtual void OnDataSet(object data)
        {
        }
        protected virtual void OnShow()
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