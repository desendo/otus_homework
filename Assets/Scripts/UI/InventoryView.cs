using DependencyInjection;
using GameManager;
using UnityEngine;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryView;
        private GameStateManager _gameStateManager;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.I) && _gameStateManager.State.Value == LevelState.Started)
                _inventoryView.SetActive(!_inventoryView.activeSelf);
        }

        [Inject]
        private void Construct(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            gameStateManager.State.OnChanged.Subscribe(x => _inventoryView.SetActive(false));
            _inventoryView.SetActive(false);
        }
    }
}