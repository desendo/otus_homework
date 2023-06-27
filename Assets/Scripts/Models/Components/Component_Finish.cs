using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using Common.Entities;

namespace Models.Components
{
    public sealed class Component_Finish
    {
        public event Action<IEntity> OnFinish;
        public readonly AtomicVariable<float> TimeLeft;

        public Component_Finish(IEntity entity, AtomicEvent onFinish, AtomicVariable<float> timeLeft)
        {

            TimeLeft = timeLeft;
            onFinish.Subscribe(() => OnFinish?.Invoke(entity));
        }
    }
}