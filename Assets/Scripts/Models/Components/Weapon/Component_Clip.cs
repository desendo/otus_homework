using Common.Atomic.Values;

namespace Models.Components
{
    //todo инкапсулировать модель
    public sealed class Component_Clip
    {
        public AtomicVariable<int> ShotsLeft { get; }
        public AtomicVariable<int> ClipSize { get; }
        public Component_Clip(AtomicVariable<int> clipSize, AtomicVariable<int> shotsLeft)
        {
            ClipSize = clipSize;
            ShotsLeft = shotsLeft;
        }
    }
}