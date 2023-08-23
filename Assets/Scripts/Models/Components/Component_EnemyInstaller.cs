using Common.Atomic.Values;
using Config;

namespace Models.Components
{
    public sealed class Component_EnemyInstaller
    {
        private readonly AtomicVariable<float> _moveModelSpeed;
        private readonly AtomicVariable<float> _healthMax;
        private readonly AtomicVariable<float> _reloadSpeedMultiplier;

        public Component_EnemyInstaller(AtomicVariable<float> moveModelSpeed,
            AtomicVariable<float> healthMax, AtomicVariable<float> reloadSpeedMultiplier)
        {
            _healthMax = healthMax;
            _moveModelSpeed = moveModelSpeed;
            _reloadSpeedMultiplier = reloadSpeedMultiplier;
        }

        public void Setup(GameConfig gameConfig)
        {
            _healthMax.Value = gameConfig.ZombieHealth;
            _moveModelSpeed.Value = gameConfig.ZombieMoveSpeed;
            _reloadSpeedMultiplier.Value = gameConfig.ZombieMoveSpeedReloadMultiplier;
        }
    }
}