using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative
{
    public class LifeModel
    {
        public AtomicEvent<int> OnTakeDamage = new AtomicEvent<int>();
        public readonly AtomicVariable<int> HitPoints = new AtomicVariable<int>();
        public readonly AtomicVariable<bool> IsDead = new AtomicVariable<bool>();

        public void Construct()
        {
            OnTakeDamage += damage => HitPoints.Value -= damage;
            HitPoints.OnChanged += hitPoints =>
            {
                if (hitPoints <= 0) IsDead.Value = true;
            };
        }
    }
}