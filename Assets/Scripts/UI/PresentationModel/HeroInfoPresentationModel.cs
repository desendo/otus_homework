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
        public AtomicVariable<int> HpCurrent { get; } = new AtomicVariable<int>();
        public AtomicVariable<int> HpMax { get; } = new AtomicVariable<int>();

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public HeroInfoPresentationModel(HeroService heroService)
        {
            heroService.HeroEntity.OnChanged.Subscribe(OnHero);
        }

        private void OnHero(EntityMono obj)
        {
            _subs.Clear();
            if(obj == null)
                return;

            var health = obj.Get<Component_Health>();

            health.Current.OnChanged.Subscribe(x => HpCurrent.Value = x);
            health.Max.OnChanged.Subscribe(x => HpMax.Value = x);
            HpCurrent.Value = health.Current.Value;
            HpMax.Value = health.Max.Value;
        }
    }
}