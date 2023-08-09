
using System;
using DependencyInjection;
using GameManager;
using ItemInventory.UI;

using UnityEngine;

namespace UI
{
    public class InventoryCanvasView : MonoBehaviour
    {

        [SerializeField] private InventoryView _inventoryView;

        [Inject]
        void Construct(GameStateManager gameStateManager)
        {

            gameStateManager.State.OnChanged.Subscribe(UpdateVisibility);
            UpdateVisibility(gameStateManager.State.Value);
        }

        private void UpdateVisibility(LevelState obj)
        {
            _inventoryView.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.I))
            {
                _inventoryView.gameObject.SetActive(!_inventoryView.gameObject.activeSelf);
            }
        }
    }
}
