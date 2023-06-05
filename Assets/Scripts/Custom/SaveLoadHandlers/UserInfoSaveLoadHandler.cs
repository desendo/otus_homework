using System.Linq;
using Custom.Config;
using Custom.Data;

namespace Custom.SaveLoadHandlers
{
    public class UserInfoSaveLoadHandler : IDataLoadHandler<GameSaveData>
    {
        private readonly UserInfo _userInfo;
        private readonly VisualConfig _visualConfig;

        public UserInfoSaveLoadHandler(UserInfo userInfo, VisualConfig visualConfig)
        {
            _userInfo = userInfo;
            _visualConfig = visualConfig;
        }

        public void SaveToData(GameSaveData data)
        {
            data.UserInfoSaveData.Description = _userInfo.Description;
            data.UserInfoSaveData.Name = _userInfo.Name;
            data.UserInfoSaveData.AvatarId =
                _visualConfig.SpriteEntries.FirstOrDefault(x => x.Sprite == _userInfo.Icon)?.Id;

        }
        public void LoadFromData(GameSaveData data)
        {
            //т.к. храним ссылку на спрайт в модели то делаем так:
            var sprite = _visualConfig.SpriteEntries.FirstOrDefault(x => x.Id == data.UserInfoSaveData.AvatarId)?.Sprite;
            _userInfo.ChangeDescription(data.UserInfoSaveData.Description);
            _userInfo.ChangeName(data.UserInfoSaveData.Name);
            _userInfo.ChangeIcon(sprite);
        }
    }
}