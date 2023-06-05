using System;
using System.Collections.Generic;
using System.Linq;
using Custom.Config;
using UnityEngine;

namespace Custom.View.PresentationModel
{
    public interface IUserInfoPresentationModel : ISelectableIconsPresentationModel, INamePresentationModel
    {
        event Action OnChanged;
        string GetDescription();
        Sprite GetIcon();
        void SetIcon(string id);
        void SetDescription(string desc);
    }

    public class UserInfoPresentationModel : IUserInfoPresentationModel
    {
        private readonly UserInfo _userInfo;
        private readonly VisualConfig _visualConfig;
        private string _temporaryAvatarId;
        private string _temporaryName;

        public event Action OnChanged;
        public event Action<string> OnIconSelected;

        public List<SpriteEntry> IconsEntries { get; } = new List<SpriteEntry>();


        public UserInfoPresentationModel(UserInfo userInfo, VisualConfig visualConfig, GameConfig gameConfig)
        {
            _visualConfig = visualConfig;
            _userInfo = userInfo;
            _userInfo.OnDescriptionChanged += x => OnChanged?.Invoke();
            _userInfo.OnNameChanged += x => OnChanged?.Invoke();
            _userInfo.OnIconChanged += x => OnChanged?.Invoke();
            foreach (var id in gameConfig.Avatars)
            {
                IconsEntries.Add(new SpriteEntry()
                {
                    Id = id,
                    //мы не копируем и не используем напрямую коллекцию SpriteEntries из visualConfig
                    //т.е. SpriteEntries может хранить в себе и другие пары id-sprite, не только аватары
                    Sprite = _visualConfig.SpriteEntries.FirstOrDefault(x=>x.Id == id)?.Sprite
                });
            }

            SetTemporaryName(_userInfo.Name);
            SetIconSelected(IconsEntries.FirstOrDefault(x=>x.Sprite == _userInfo.Icon)?.Id);
        }
        public string GetName()
        {
            return _userInfo.Name;
        }
        public string GetDescription()
        {
            return _userInfo.Description;
        }
        public Sprite GetIcon()
        {
            return _userInfo.Icon;
        }

        public void SetIcon(string id)
        {
            _userInfo.ChangeIcon(_visualConfig.SpriteEntries.FirstOrDefault(x => x.Id == id)?.Sprite);
        }

        public void SetName(string name)
        {
            _userInfo.ChangeName(name);
        }

        public void SetTemporaryName(string name)
        {
            _temporaryName = name;
        }

        public void SetDescription(string desc)
        {
            _userInfo.ChangeDescription(desc);
        }
        public void SetIconSelected(string id)
        {
            _temporaryAvatarId = id;
            OnIconSelected?.Invoke(id);
        }

        public string GetCurrentIcon()
        {
            return _visualConfig.SpriteEntries.FirstOrDefault(x => x.Sprite == _userInfo.Icon)?.Id;
        }

        public void ApplyValues()
        {
            SetIcon(_temporaryAvatarId);
            SetName(_temporaryName);
        }
    }
}