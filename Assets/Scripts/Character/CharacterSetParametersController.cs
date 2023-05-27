using Components;
using Config;
using Input;

namespace Character
{
    public sealed class CharacterSetParametersController : IStartGame
    {
        private readonly CharacterMono _characterMono;
        private readonly GameConfig _gameConfig;

        public CharacterSetParametersController(CharacterMono characterMono, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _characterMono = characterMono;
        }

        public void StartGame()
        {
            _characterMono.GetComponent<HitPointsComponent>()
                .SetHitPoints(_gameConfig.CharHealth, _gameConfig.CharHealth);
            _characterMono.GetComponent<WeaponComponent>()
                .SetBulletConfig(_gameConfig.PlayerBulletConfig);
        }
    }
}