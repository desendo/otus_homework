using Common.Atomic.Values;
using Config;

namespace Models.Components
{
    public sealed class Component_EnemyInstaller
    {
        private readonly AtomicVariable<float> _moveModelSpeed;
        private readonly AtomicVariable<int> _healthMax;

        public Component_EnemyInstaller(AtomicVariable<float> moveModelSpeed,
            AtomicVariable<int> healthMax)
        {
            _healthMax = healthMax;
            _moveModelSpeed = moveModelSpeed;
        }

        public void Setup(GameConfig gameConfig)
        {
            _healthMax.Value = gameConfig.ZombieHealth;
            _moveModelSpeed.Value = gameConfig.ZombieMoveSpeed;
        }
    }
}