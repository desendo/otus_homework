using System;
using Custom.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Custom.Services
{
    public interface IGameSaveDataService
    {
        public void Load(Action<GameSaveData> onLoad);
        public void LoadDefault(Action<GameSaveData> onLoad);
        public void SaveData();
        public GameSaveData Data { get; }
    }

    public sealed class GameSaveDataService : IGameSaveDataService
    {
        public GameSaveData Data { get; private set; }
        private readonly GameSaveDataContainer _defaultDataContainer;
        private const string SavePrefsKey = "save";

        public GameSaveDataService(GameSaveDataContainer defaultDataContainer)
        {
            //здесь можно сделать систему хендлеров (для префсов, SO, сервер и т.п.), но не в рамках этого дз
            _defaultDataContainer = defaultDataContainer;
            Data =  new GameSaveData();
        }

        public void Load(Action<GameSaveData> onLoad)
        {
            var json = PlayerPrefs.GetString(SavePrefsKey);
            if (string.IsNullOrEmpty(json))
            {
                LoadDefault(onLoad);
                return;
            }
            var deserializedObject = JsonConvert.DeserializeObject<GameSaveData>(json);
            if (deserializedObject == null)
            {
                LoadDefault(onLoad);
                return;
            }
            onLoad.Invoke(deserializedObject);
        }
        public void LoadDefault(Action<GameSaveData> onLoad)
        {
            var defaultDataSerialized = JsonConvert.SerializeObject(_defaultDataContainer.Data);
            var defaultData = JsonConvert.DeserializeObject<GameSaveData>(defaultDataSerialized);
            onLoad.Invoke(defaultData);
        }

        public void SaveData()
        {
            var json = JsonConvert.SerializeObject(Data);
            PlayerPrefs.SetString(SavePrefsKey, json);
        }

    }
}