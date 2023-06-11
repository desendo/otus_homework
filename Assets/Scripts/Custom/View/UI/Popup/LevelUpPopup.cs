using System;
using Custom.View.PresentationModel;
using Custom.View.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Popup
{
    public class LevelUpPopup : PopupBase
    {
        [SerializeField] private TitleWidget _titleWidget;
        [SerializeField] private ButtonWidget _levelUpButtonWidget;
        [SerializeField] private ProgressBarWidget _progressBarWidget;
        [SerializeField] private InfoWidget _infoWidget;
        [SerializeField] private NameValueListWidget _statListWidget;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _closeBackButton;

        private LevelUpPresentationModel _presentationModel;

        public void Awake()
        {
            _closeButton.onClick.AddListener(TryClose);
            _closeBackButton.onClick.AddListener(TryClose);
        }

        protected override void OnShow(object data)
        {
            if (!(data is LevelUpPresentationModel presentationModel))
                throw new Exception($"expected {typeof(LevelUpPresentationModel)} as data");

            _presentationModel = presentationModel;
            _levelUpButtonWidget.OnClickEvent += presentationModel.ExperiencePresentationModel.LevelUp;

            _presentationModel.UserInfoPresentationModel.OnChanged += UpdateUserInfo;
            _presentationModel.ExperiencePresentationModel.OnChanged += UpdateExperienceInfo;
            _presentationModel.CharacterInfoPresentationModel.OnStatAdd += AddStat;
            _presentationModel.CharacterInfoPresentationModel.OnStatRemove += RemoveStat;

            foreach (var statPm in _presentationModel.CharacterInfoPresentationModel.StatsPmList)
            {
                AddStat(statPm);
            }
            UpdateExperienceInfo();
            UpdateUserInfo();
        }

        private void AddStat(INameValuePresentationModel obj)
        {
            _statListWidget.AddWidget(obj);
        }
        private void RemoveStat(INameValuePresentationModel obj)
        {
            _statListWidget.RemoveWidget(obj);
        }
        private void UpdateExperienceInfo()
        {
            _infoWidget.SetTitle(_presentationModel.ExperiencePresentationModel.GetLevelText());
            _levelUpButtonWidget.SetInteractable(_presentationModel.ExperiencePresentationModel.IsComplete);
            _progressBarWidget.SetFill(_presentationModel.ExperiencePresentationModel.GetProgressValue());
            _progressBarWidget.SetFillComplete(_presentationModel.ExperiencePresentationModel.IsComplete);
            _progressBarWidget.SetText(_presentationModel.ExperiencePresentationModel.GetExperienceText());
        }

        private void UpdateUserInfo()
        {
            _infoWidget.SetDescription(_presentationModel.UserInfoPresentationModel.GetDescription());
            _infoWidget.SetIcon(_presentationModel.UserInfoPresentationModel.GetIcon());
            _titleWidget.SetTitle(_presentationModel.UserInfoPresentationModel.GetName());
        }

        protected override void OnHide()
        {
            _statListWidget.Dispose();
            _levelUpButtonWidget.OnClickEvent -= _presentationModel.ExperiencePresentationModel.LevelUp;
            _presentationModel.UserInfoPresentationModel.OnChanged -= UpdateUserInfo;
            _presentationModel.ExperiencePresentationModel.OnChanged -= UpdateExperienceInfo;
            _presentationModel.CharacterInfoPresentationModel.OnStatAdd -= AddStat;
            _presentationModel.CharacterInfoPresentationModel.OnStatRemove -= RemoveStat;
        }
    }

}