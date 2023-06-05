using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    public class SelectIconButtonWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selected;

        private string _id;
        public event Action<string> OnClick;
        public string Id => _id;

        public void SetSelected(bool isSelected)
        {
            _selected.SetActive(isSelected);
        }
        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
        public void SetId(string id)
        {
            _id = id;
        }
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Click);
        }

        private void Click()
        {
            OnClick?.Invoke(_id);
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }

        public void Set(Sprite icon, string id, bool isSelected)
        {
            gameObject.SetActive(true);
            SetIcon(icon);
            SetId(id);
            SetSelected(isSelected);
        }
    }
}