using Common.Atomic.Values;

namespace Models.Components
{
    public class Component_Damage
    {
        public AtomicVariable<int> Damage { get; }
        public Component_Damage(AtomicVariable<int> damage)
        {
            Damage = damage;
        }
    }

}