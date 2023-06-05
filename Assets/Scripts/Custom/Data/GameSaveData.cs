using System;
using System.Collections.Generic;

namespace Custom.Data
{
    [Serializable]
    public class GameSaveData
    {
        private int Version;
        public CharacterSaveData CharacterSaveData = new CharacterSaveData();
        public UserInfoSaveData UserInfoSaveData = new UserInfoSaveData();
        public PlayerSaveData PlayerSaveData = new PlayerSaveData();
    }
    [Serializable]
    public class CharacterSaveData
    {
        public List<StatSaveData> StatList = new List<StatSaveData>();
    }
    [Serializable]
    public class StatSaveData
    {
        public string Name;
        public int Value;
    }
    [Serializable]
    public class PlayerSaveData
    {
        public int CurrentLevel;
        public int CurrentExperience;
    }
    [Serializable]
    public class UserInfoSaveData
    {
        public string AvatarId;
        public string Name;
        public string Description;
    }
}