using System;
using System.Collections.Generic;
using Common.Atomic.Values;
using Common.Entities;
using Models.Components;
using Services;

namespace UI.PresentationModel
{
    public class HeroInfoPresentationModel
    {
        public AtomicVariable<int> Current { get; } = new AtomicVariable<int>();
        public AtomicVariable<int> Max { get; } = new AtomicVariable<int>();

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public HeroInfoPresentationModel(HeroService heroService)
        {
            heroService.HeroEntity.OnChanged.Subscribe(OnHero);
        }

        private void OnHero(IEntity obj)
        {
            _subs.Clear();
            if(obj == null)
                return;

            var health = obj.Get<Component_Health>();

            health.Current.OnChanged.Subscribe(x => Current.Value = x);
            health.Max.OnChanged.Subscribe(x => Max.Value = x);
            Current.Value = health.Current.Value;
            Max.Value = health.Max.Value;
        }
    }
}