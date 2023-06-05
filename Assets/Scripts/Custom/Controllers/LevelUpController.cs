
namespace Custom.Controllers
{
    public class LevelUpController
    {
        private readonly CharacterInfo _characterInfo;

        public LevelUpController(PlayerLevel playerLevel, CharacterInfo characterInfo)
        {
            playerLevel.OnLevelUp += OnLevelUp;
            _characterInfo = characterInfo;
        }

        //при апгрейде уровня повышаем рандомную стату (гипотетическая механика)
        private void OnLevelUp()
        {
            var stats = _characterInfo.GetStats();
            var statsCount = stats.Length;
            var randomIndex = UnityEngine.Random.Range(0, statsCount);
            var stat = stats[randomIndex];
            stat.ChangeValue(stat.Value + 1);
        }
    }
}