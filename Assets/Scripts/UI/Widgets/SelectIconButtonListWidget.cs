using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Custom.View.UI.Widgets;
using UI.PresentationModel;
using UnityEngine;

namespace UI.Widgets
{

    public class SelectIconButtonListWidget : MonoBehaviour
    {
        private List<SelectIconButtonWidget> _list;
        private ISelectableIconsPresentationModel _pm;

        private void Awake()
        {
            Initialize();
        }

        public void Bind(ISelectableIconsPresentationModel userInfoPresentationModel)
        {
            _pm = userInfoPresentationModel;
            UpdateElementsCount(_pm.IconsEntries);

            _pm.OnIconSelected += OnIconSelected;
            var currentIconId = _pm.GetCurrentIcon();
            for (var i = 0; i < _pm.IconsEntries.Count; i++)
            {
                var spriteEntry = _pm.IconsEntries[i];
                var element = _list[i];
                var isSelected = currentIconId == spriteEntry.Id;
                element.Set(spriteEntry.Sprite, spriteEntry.Id, isSelected);
                element.OnClick += OnClick;
            }
        }

        private void OnIconSelected(string obj)
        {
            foreach (var element in _list)
                element.SetSelected(element.Id == obj);
        }

        private void OnClick(string id)
        {
            //здесь мы колбек гоняем по кругу, но возможно согласно какой то логике нужно будет завернуть на родительский виджет, но пока это не нужно
            _pm.SetIconSelected(id);
        }

        private void OnDisable()
        {
            foreach (var element in _list)
            {
                element.OnClick -= OnClick;
            }
            _pm.OnIconSelected -= OnIconSelected;
        }

        private void UpdateElementsCount(ICollection entries)
        {
            if (_list.Count < entries.Count)
            {
                var needAdd = entries.Count - _list.Count;
                for (int i = 0; i < needAdd; i++)
                {
                    var additionalElement = Instantiate(_list[0], transform);
                    additionalElement.Reset();
                    _list.Add(additionalElement);
                }
            }
        }

        private void Initialize()
        {
            _list = GetComponentsInChildren<SelectIconButtonWidget>().ToList();
            foreach (var element in _list)
            {
                element.Reset();
            }
        }
    }
}