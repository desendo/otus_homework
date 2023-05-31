using Components;
using Config;

namespace Character
{
    public sealed class CharacterInstaller : IStartGame
    {
        private readonly CharacterService _characterService;
        private readonly GameConfig _gameConfig;

        public CharacterInstaller(CharacterService characterService, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _characterService = characterService;
        }

        public void StartGame()
        {
            _characterService.Character.SetActive(true);
            _characterService.Character.GetComponent<HitPointsComponent>()
                .SetHitPoints(_gameConfig.CharHealth);
            _characterService.Character.GetComponent<WeaponComponent>()
                .SetBulletConfig(_gameConfig.PlayerBulletConfig);
        }
    }
}