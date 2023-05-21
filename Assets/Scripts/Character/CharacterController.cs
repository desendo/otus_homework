using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : IStartGame, IFinishGame, IFixedUpdate
    {
        private readonly Character _character;
        private readonly GameStateService _gameStateService;
        private readonly BulletSystem _bulletSystem;
        private readonly BulletConfig _bulletConfig;

        public bool _fireRequired;

        public CharacterController(Character character, GameStateService gameStateService, BulletSystem bulletSystem, BulletConfig bulletConfig)
        {
            _character = character;
            _gameStateService = gameStateService;
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;
        }

        public void FixedUpdate(float dt)
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        public void StartGame()
        {
            _character.DoReset();
            _character.HitPointsComponent.hpEmpty += OnCharacterDeath;
        }

        public void FinishGame()
        {
            _character.HitPointsComponent.hpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _gameStateService.SetGameFinished();
        }

        private void OnFlyBullet()
        {
            var weapon = _character.WeaponComponent;
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) _bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.speed
            });
        }



    }
}