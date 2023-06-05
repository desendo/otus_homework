using Custom.Data;

namespace Custom.SaveLoadHandlers
{
    public class PlayerLevelSaveLoadHandler : IDataLoadHandler<GameSaveData>
    {
        private readonly PlayerLevel _playerLevel;

        public PlayerLevelSaveLoadHandler(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;
        }

        public void SaveToData(GameSaveData data)
        {
            data.PlayerSaveData.CurrentExperience = _playerLevel.CurrentExperience;
            data.PlayerSaveData.CurrentLevel = _playerLevel.CurrentLevel;
        }

        public void LoadFromData(GameSaveData data)
        {
            _playerLevel.SetExperience(data.PlayerSaveData.CurrentExperience);
            _playerLevel.SetLevel(data.PlayerSaveData.CurrentLevel);
        }
    }
}