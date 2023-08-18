using System.Collections.Generic;
using DependencyInjection;
using ItemInventory;
using UnityEngine;

public class InventoryInstaller : MonoBehaviour, IStartGameListener, IFinishGameListener
{
    [SerializeField] private List<ItemConfig> _initialItemsConfigs;
    private Inventory _inventory;

    [Inject]
    public void Construct(Inventory inventory)
    {
        _inventory = inventory;
    }

    public void OnStartGame()
    {
        foreach (var initialItemsConfig in _initialItemsConfigs)
        {
            _inventory.AddItem(initialItemsConfig.CreateItem());
        }
    }

    public void OnFinishGame(bool gameWin)
    {
        _inventory.Clear();
    }
}