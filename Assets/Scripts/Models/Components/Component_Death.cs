using System;
using Common.Atomic.Actions;
using Common.Entities;

namespace Models.Components
{
    public sealed class Component_Death
    {
        public AtomicEvent<IEntity> OnDeath = new AtomicEvent<IEntity>();
        private Action<IEntity> _onDeath;

        public Component_Death(AtomicEvent onDeath, IEntity entity)
        {
            onDeath.Subscribe(() =>
            {
                OnDeath.Invoke(entity);
                _onDeath?.Invoke(entity);
            });
        }

        public void SetCallback(Action<IEntity> onDeath)
        {
            _onDeath = onDeath;
        }
    }
}