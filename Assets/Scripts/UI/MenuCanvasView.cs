using System;
using DependencyInjection;
using GameManager;
using Signals;
using UnityEngine;

namespace UI
{
    public class MenuCanvasView : MonoBehaviour
    {
        [SerializeField] private GameObject _noneStateContainer;
        [SerializeField] private GameObject _winStateContainer;
        [SerializeField] private GameObject _lostStateContainer;
        [Inject]
        void Construct(GameStateManager gameStateManager, SignalBusService signalBusService)
        {
            gameStateManager.State.OnChanged.Subscribe(UpdateState);
            UpdateState(gameStateManager.State.Value);
        }

        private void UpdateState(LevelState state)
        {
            _noneStateContainer.SetActive(false);
            _winStateContainer.SetActive(false);
            _lostStateContainer.SetActive(false);

            if (state == LevelState.None) _noneStateContainer.SetActive(true);
            if (state == LevelState.Lost) _lostStateContainer.SetActive(true);
            if (state == LevelState.Win) _winStateContainer.SetActive(true);
        }
    }
}
