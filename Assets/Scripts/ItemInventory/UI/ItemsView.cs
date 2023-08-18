using System;
using System.Collections.Generic;
using Config;
using DependencyInjection;
using Pool;
using UI.PresentationModel;
using UnityEngine;

namespace ItemInventory.UI
{
    public class ItemsView : MonoBehaviour
    {
        [SerializeField] private Transform _holder;
        [SerializeField] private Transform _dragContainer;

        private readonly List<InventoryItemView> _itemViews = new List<InventoryItemView>();

        private VisualConfig _visualConfig;
        private InventoryPresentationModel _pm;
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private InventoryItemViewPool _pool;

        [Inject]
        public void Construct(InventoryPresentationModel inventoryPresentationModel,
            InventoryItemViewPool inventoryItemViewPool)
        {
            _pool = inventoryItemViewPool;
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
                var view = _pool.Spawn();
                view.Setup(itemPm, _dragContainer);
                view.transform.SetParent(_holder);
                _itemViews.Add(view);
            }
        }

        private void Clear()
        {
            foreach (var inventoryItemView in _itemViews)
            {
                _pool.Unspawn(inventoryItemView);
            }
            _itemViews.Clear();
        }
    }
}
