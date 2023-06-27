using System;
using System.Collections.Generic;
using DependencyInjection;
using GameState;
using Pool;
using UI.PresentationModel;
using UnityEngine;

namespace UI
{
    public class InfoCanvasView : MonoBehaviour
    {
        [SerializeField] private WeaponUIViewPool _pool;


        private WeaponsListPresentationModel _weaponsListPresentationModel;
        private IDisposable _changeSub;
        private readonly List<WeaponUIView> _weaponUIViews = new List<WeaponUIView>();

        [Inject]
        void Construct(GameStateManager gameStateManager, WeaponsListPresentationModel weaponsListPresentationModel)
        {
            _weaponsListPresentationModel = weaponsListPresentationModel;
            gameStateManager.GameLoaded.OnChanged.Subscribe(UpdateGameState);
        }

        private void UpdateGameState(bool gameLoaded)
        {
            _changeSub?.Dispose();
            if(!gameLoaded)
                return;

            _changeSub = _weaponsListPresentationModel.OnChange.Subscribe(UpdateList);
            UpdateList();
        }

        private void UpdateList()
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
