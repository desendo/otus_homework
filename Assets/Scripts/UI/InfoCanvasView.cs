using System;
using System.Collections.Generic;
using Common;
using DependencyInjection;
using GameManager;
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

        private HeroInfoPresentationModel _hpPm;
        private WeaponsListPresentationModel _weaponsListPresentationModel;
        private readonly List<IDisposable> _changeSub = new List<IDisposable>();
        private readonly List<WeaponUIView> _weaponUIViews = new List<WeaponUIView>();
        private KillsPresentationModel _killsPm;

        [Inject]
        void Construct(GameStateManager gameStateManager,
            WeaponsListPresentationModel weaponsListPresentationModel,
            HeroInfoPresentationModel hpPm,
            KillsPresentationModel killsPm)
        {
            _killsPm = killsPm;
            _hpPm = hpPm;
            _weaponsListPresentationModel = weaponsListPresentationModel;
            gameStateManager.GameLoaded.OnChanged.Subscribe(UpdateGameState);
            gameStateManager.State.OnChanged.Subscribe(UpdateVisibility);

            UpdateVisibility(gameStateManager.State.Value);
            UpdateGameState(gameStateManager.GameLoaded.Value);
        }

        private void UpdateVisibility(LevelState obj)
        {
            _hp.gameObject.SetActive(obj == LevelState.Started);
            _killProgress.gameObject.SetActive(obj == LevelState.Started);
        }

        private void UpdateGameState(bool gameLoaded)
        {
            _changeSub?.Dispose();
            if(!gameLoaded)
                return;

            _weaponsListPresentationModel.OnChange.Subscribe(UpdateWeaponList).AddTo(_changeSub);
            _hpPm.Current.OnChanged.Subscribe(x => UpdateHealthBar()).AddTo(_changeSub);
            _hpPm.Max.OnChanged.Subscribe(x => UpdateHealthBar()).AddTo(_changeSub);

            _killsPm.Current.OnChanged.Subscribe(x=>UpdateKills()).AddTo(_changeSub);
            _killsPm.Max.OnChanged.Subscribe(x=>UpdateKills()).AddTo(_changeSub);

            UpdateHealthBar();
            UpdateKills();
            UpdateWeaponList();
        }

        private void UpdateKills()
        {
            _killProgress.SetFill((float)_killsPm.Current.Value/_killsPm.Max.Value);
            _killProgress.SetText($"{_killsPm.Current.Value}/{_killsPm.Max.Value}");
        }

        private void UpdateHealthBar()
        {
            _hp.SetFill((float) _hpPm.Current.Value/_hpPm.Max.Value);
            _hp.SetText($"{_hpPm.Current.Value}/{_hpPm.Max.Value}");
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
