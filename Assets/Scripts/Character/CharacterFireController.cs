using Bullets;
using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class CharacterFireController : IStartGame, IFinishGame, IUpdate
    {
        private readonly BulletSystem _bulletSystem;
        private readonly CharacterMono _characterMono;
        private readonly InputManager _inputManager;
        private bool _started;

        public CharacterFireController(InputManager inputManager, CharacterMono characterMono,
            BulletSystem bulletSystem)
        {
            _inputManager = inputManager;
            _characterMono = characterMono;
            _bulletSystem = bulletSystem;
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
            var weapon = _characterMono.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
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