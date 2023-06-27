using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Info
    {

        public AtomicVariable<string> Name;
        public AtomicVariable<string> Description;

        public Component_Info(AtomicVariable<string> name, AtomicVariable<string> description = null)
        {
            Name = name;
            Description = description;

        }
    }
}