using System;
using System.Collections.Generic;
using Homework.Data;

namespace Homework
{
    public class PlayerResourcesSaveLoader : ISaveLoader
    {
        private readonly PlayerResources _playerResources;
        private readonly ISaveDataProvider _saveDataProvider;

        public PlayerResourcesSaveLoader(ISaveDataProvider saveDataProvider, PlayerResources playerResources)
        {
            _saveDataProvider = saveDataProvider;
            _playerResources = playerResources;
        }

        public bool TryLoad(ISaveDataProvider saveDataProvider)
        {
            if (_saveDataProvider.TryGetData<List<PlayerResourceData>>(out var data))
            {
                foreach (var item in data)
                {
                    _playerResources.SetResource(item.ResourceType, item.Value);
                }

                return true;
            }

            return false;
        }

        public void Save(ISaveDataProvider saveDataProvider)
        {
            var data = new List<PlayerResourceData>();
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
                data.Add(new PlayerResourceData
                {
                    Value = _playerResources.GetResource(type), ResourceType = type
                });

            _saveDataProvider.SetData(data);
        }
    }
}