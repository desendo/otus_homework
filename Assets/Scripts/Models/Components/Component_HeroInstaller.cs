using Common.Atomic.Values;
using Config;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_HeroInstaller
    {
        private readonly AtomicVariable<float> _moveModelSpeed;
        private readonly AtomicVariable<float> _moveModelRotationSpeed;

        public Component_HeroInstaller(AtomicVariable<float> moveModelSpeed, AtomicVariable<float> moveModelRotationSpeed)
        {
            _moveModelSpeed = moveModelSpeed;
            _moveModelRotationSpeed = moveModelRotationSpeed;
        }

        public void Setup(GameConfig gameConfig)
        {
            _moveModelSpeed.Value = gameConfig.PlayerMoveSpeed;
            _moveModelRotationSpeed.Value = gameConfig.PlayerRotationSpeed;
        }
    }

}