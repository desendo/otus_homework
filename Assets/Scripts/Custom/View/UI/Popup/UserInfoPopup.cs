﻿using System;
using Custom.View.PresentationModel;
using Custom.View.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Popup
{
    public class UserInfoPopup : PopupBase
    {
        [SerializeField] private SelectIconButtonListWidget _iconsWidget;
        [SerializeField] private ButtonWidget _saveButtonWidget;
        [SerializeField] private TMP_InputField _nameInputField;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeBackButton;

        private IUserInfoPresentationModel _presentationModel;

        public void Awake()
        {
            _closeButton.onClick.AddListener(TryClose);
            _closeBackButton.onClick.AddListener(TryClose);
            _saveButtonWidget.OnClickEvent += TryClose;
        }

        protected override void OnShow(object data)
        {
            if (!(data is IUserInfoPresentationModel presentationModel))
                throw new Exception($"expected {typeof(IUserInfoPresentationModel)} as data");

            _presentationModel = presentationModel;
            _nameInputField.text = presentationModel.GetName();
            _iconsWidget.Bind(_presentationModel);
            _saveButtonWidget.OnClickEvent += _presentationModel.ApplyValues;
        }

        public void OnEndNameEdit()
        {
            _presentationModel?.SetTemporaryName(_nameInputField.text);
        }

        protected override void OnHide()
        {
            _saveButtonWidget.OnClickEvent -= _presentationModel.ApplyValues;
        }
    }
}