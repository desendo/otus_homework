using System;
using Common.Atomic.Values;

namespace Models.Declarative.Weapons
{
    public class Burst : IDisposable
    {
        public readonly AtomicVariable<float> BurstAngle = new AtomicVariable<float>();
        public readonly AtomicVariable<int> BurstCount = new AtomicVariable<int>();

        public void Dispose()
        {
        }
    }
}