
using Custom.Data;

namespace Custom.SaveLoadHandlers
{
    public class CharacterSaveLoadHandler : IDataLoadHandler<GameSaveData>
    {
        private readonly CharacterInfo _characterInfo;

        public CharacterSaveLoadHandler(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
        }

        //логику методов LoadFromData SaveToData можно вынести в дата-адаптеры, но не в этом дз
        public void SaveToData(GameSaveData data)
        {
            var stats = _characterInfo.GetStats();
            data.CharacterSaveData.StatList.Clear();
            foreach (var characterStat in stats)
            {
                var stat = new CharacterStat(characterStat.Name, characterStat.Value);
                data.CharacterSaveData.StatList.Add(new StatSaveData()
                {
                    Name = stat.Name,
                    Value = stat.Value
                });
            }
        }

        public void LoadFromData(GameSaveData data)
        {
            var stats = _characterInfo.GetStats();
            foreach (var characterStat in stats)
            {
                _characterInfo.RemoveStat(characterStat);
            }
            foreach (var statSaveData in data.CharacterSaveData.StatList)
            {
                _characterInfo.AddStat(new CharacterStat(statSaveData.Name, statSaveData.Value));
            }
        }
    }
}