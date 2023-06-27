using System;
using Common.Atomic.Values;

namespace Models.Declarative.Weapons
{
    public class  ClipModel : IDisposable
    {

        public readonly AtomicVariable<int> ClipSize = new AtomicVariable<int>();
        public readonly AtomicVariable<int> ShotsLeft = new AtomicVariable<int>();

        public ClipModel()
        {
            ClipSize.OnChanged.Subscribe(x => ShotsLeft.Value = x);

        }

        public void Dispose()
        {
        }
    }
}