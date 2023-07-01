using Common.Atomic.Values;
using Config;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_BulletInstaller
    {
        private readonly AtomicVariable<int> _damage;
        private readonly AtomicVariable<Vector3> _direction;

        public Component_BulletInstaller(AtomicVariable<int> damage)
        {
            _damage = damage;

        }

        public void Setup(int damage, Vector3 velocity)
        {
            _damage.Value = damage;
            _direction.Value = velocity;
        }
    }
}