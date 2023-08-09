using System;
using System.Collections.Generic;
using Config;
using DependencyInjection;
using Pool;
using UI.PresentationModel;
using UnityEngine;

namespace ItemInventory.UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _holder;
        
        private VisualConfig _visualConfig;
        private InventoryPresentationModel _pm;
        private readonly List<IDisposable> _subs = new List<IDisposable>();

        [Inject]
        public void Construct(InventoryPresentationModel inventoryPresentationModel, InventoryItemViewPool inventoryItemViewPool)
        {
            _pm = inventoryPresentationModel;
        }

        private void OnEnable()
        {
            Populate();
        }

        private void Populate()
        {
            Clear();
            foreach (var itemPm in _pm.ItemPms)
            {
            }
        }

        private void Clear()
        {
        }
    }
}
