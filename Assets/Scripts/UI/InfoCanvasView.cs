using System;
using System.Collections.Generic;
using Common;
using DependencyInjection;
using GameState;
using Pool;
using UI.PresentationModel;
using UI.Widgets;
using UnityEngine;

namespace UI
{
    public class InfoCanvasView : MonoBehaviour
    {
        [SerializeField] private WeaponUIViewPool _pool;
        [SerializeField] private ProgressBarWidget _hp;
        [SerializeField] private ProgressBarWidget _killProgress;


        private WeaponsListPresentationModel _weaponsListPresentationModel;
        private readonly List<IDisposable> _changeSub = new List<IDisposable>();
        private readonly List<WeaponUIView> _weaponUIViews = new List<WeaponUIView>();
        private HeroInfoPresentationModel _hpPm;

        [Inject]
        void Construct(GameStateManager gameStateManager, WeaponsListPresentationModel weaponsListPresentationModel,
            HeroInfoPresentationModel hpPm)
        {
            _hpPm = hpPm;
            _weaponsListPresentationModel = weaponsListPresentationModel;
            gameStateManager.GameLoaded.OnChanged.Subscribe(UpdateGameState);
        }

        private void UpdateGameState(bool gameLoaded)
        {
            _changeSub?.Dispose();
            if(!gameLoaded)
                return;

            _weaponsListPresentationModel.OnChange.Subscribe(UpdateWeaponList).AddTo(_changeSub);
            _hpPm.HpCurrent.OnChanged.Subscribe(x => UpdateHealthBar(_hpPm.HpCurrent.Value, _hpPm.HpMax.Value)).AddTo(_changeSub);
            _hpPm.HpMax.OnChanged.Subscribe(x => UpdateHealthBar(_hpPm.HpCurrent.Value, _hpPm.HpMax.Value)).AddTo(_changeSub);
            UpdateHealthBar(_hpPm.HpCurrent.Value, _hpPm.HpMax.Value);
            UpdateWeaponList();
        }

        private void UpdateHealthBar(int hpCurrentValue, int hpMaxValue)
        {
            _hp.SetFill((float) hpCurrentValue/hpMaxValue);
            _hp.SetText($"{hpCurrentValue}/{hpMaxValue}");
        }

        private void UpdateWeaponList()
        {
            Clear();
            foreach (var pm in _weaponsListPresentationModel.WeaponPresentationModels)
            {
                var ui = _pool.Spawn();
                ui.Setup(pm);
            }
        }

        private void Clear()
        {
            foreach (var weaponUIView in _weaponUIViews)
            {
                _pool.Unspawn(weaponUIView);
            }

            _weaponUIViews.Clear();
        }
    }
}
