using System.Collections.Generic;
using Homework.Signals;

namespace Homework
{
    public class SaveLoadController
    {
        private readonly List<ISaveLoader> _saveLoaders;
        private readonly ISaveDataProvider _saveDataProvider;
        private readonly List<ISaveDataHandler> _saveDataHandlers;
        private readonly List<ISpawner> _spawners;

        public SaveLoadController(List<ISaveLoader> saveLoaders, ISaveDataProvider saveDataProvider, List<ISpawner> spawners,
            List<ISaveDataHandler> saveDataHandlers, SignalBusService signalBusService)
        {
            _spawners = spawners;
            _saveLoaders = saveLoaders;
            _saveDataProvider = saveDataProvider;
            _saveDataHandlers = saveDataHandlers;
            signalBusService.Subscribe<GameSignals.SaveRequest>(x => Save());
            signalBusService.Subscribe<GameSignals.LoadRequest>(x => Load());
        }

        public void Load()
        {
            foreach (var saveDataHandler in _saveDataHandlers)
                saveDataHandler.LoadData();

            foreach (var saveLoader in _saveLoaders)
                saveLoader.TryLoad(_saveDataProvider);
            foreach (var spawner in _spawners)
                spawner.Spawn();
        }

        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
                saveLoader.Save(_saveDataProvider);
            foreach (var saveDataHandler in _saveDataHandlers)
                saveDataHandler.SaveData();
        }
    }
}