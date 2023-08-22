using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Components
{
    public class Component_Damage
    {
        private readonly AtomicVariable<float> _multiplier;
        public AtomicVariable<float> Damage { get; } = new AtomicVariable<float>();
        public Component_Damage(AtomicVariable<float> damage, AtomicVariable<float> multiplier = null)
        {
            _multiplier = multiplier;
            _multiplier ??= new AtomicVariable<float>(1);

            Damage.Value = damage.Value * _multiplier.Value;

            damage.OnChanged.Subscribe(x =>
            {
                Damage.Value = x * _multiplier.Value;
            });
            _multiplier.OnChanged.Subscribe(x =>
            {
                Damage.Value = x * damage.Value;
            });
        }
    }
}