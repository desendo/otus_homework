using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Components
{
    public class Component_Damage
    {
        public AtomicVariable<int> Damage { get; } = new AtomicVariable<int>();
        public Component_Damage(AtomicVariable<int> damage)
        {
            Damage.Value = damage.Value;
            damage.OnChanged.Subscribe(x => Damage.Value = x);
        }
    }

}