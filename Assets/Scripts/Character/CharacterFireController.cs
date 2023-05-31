using Bullets;
using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class CharacterFireController : IStartGame, IFinishGame, IUpdate
    {
        private readonly BulletManager _bulletManager;
        private readonly CharacterService _characterService;
        private readonly InputManager _inputManager;
        private bool _started;

        public CharacterFireController(InputManager inputManager, CharacterService characterService,
            BulletManager bulletManager)
        {
            _inputManager = inputManager;
            _characterService = characterService;
            _bulletManager = bulletManager;
        }
        public void Update(float dt)
        {
            if (!_started)
                return;

            if (_inputManager.Fire)
                OnFlyBullet();
        }

        private void OnFlyBullet()
        {
            var weapon = _characterService.Character.GetComponent<WeaponComponent>();
            _bulletManager.FlyBulletByArgs(new BulletManager.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int) weapon.BulletConfig.PhysicsLayer,
                Color = weapon.BulletConfig.Color,
                Damage = weapon.BulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * weapon.BulletConfig.Speed
            });
        }

        public void FinishGame()
        {
            _started = false;
        }

        public void StartGame()
        {
            _started = true;
        }
    }
}