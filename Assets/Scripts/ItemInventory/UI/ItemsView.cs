using System;
using System.Collections.Generic;
using System.Linq;
using Config;
using DependencyInjection;
using ItemInventory.UI.PresentationModel;
using Pool;
using ReactiveExtension;
using Signals;
using UI.PresentationModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemInventory.UI
{
    public class ItemsView : MonoBehaviour,IDropHandler
    {
        [SerializeField] private Transform _holder;
        [SerializeField] private Transform _dragTransform;

        private readonly List<InventoryItemView> _itemViews = new List<InventoryItemView>();
        public IReadOnlyList<InventoryItemView> ItemViews => _itemViews;

        public Event<InventoryItemView> OnViewSpawned = new Event<InventoryItemView>();
        public Event<InventoryItemView> OnViewDespawned = new Event<InventoryItemView>();
        private VisualConfig _visualConfig;
        private InventoryPresentationModel _pm;
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private InventoryItemViewPool _pool;
        private IDisposable _onAddSub;
        private IDisposable _onRemoveSub;
        private SignalBusService _signalBusService;

        [Inject]
        public void Construct(InventoryPresentationModel inventoryPresentationModel,
            InventoryItemViewPool inventoryItemViewPool, SignalBusService signalBusService)
        {
            _signalBusService = signalBusService;
            _pool = inventoryItemViewPool;
            _pm = inventoryPresentationModel;
        }

        private void OnEnable()
        {
            Populate();
            _onAddSub?.Dispose();
            _onRemoveSub?.Dispose();
            _onAddSub = _pm.OnAdd.Subscribe(ProcessPmAdd);
            _onRemoveSub = _pm.OnRemove.Subscribe(ProcessPmRemove);
        }

        private void Populate()
        {
            Clear();
            foreach (var itemPm in _pm.ItemPms)
            {
                ProcessPmAdd(itemPm);
            }
        }

        private void ProcessPmRemove(ItemPresentationModel obj)
        {
            var view = _itemViews.FirstOrDefault(x => x.Id == obj.Id);
            if (view != null)
            {
                _itemViews.Remove(view);
                _pool.Unspawn(view);
                OnViewDespawned.Invoke(view);
            }
        }

        private void ProcessPmAdd(ItemPresentationModel itemPm)
        {
            var view = _pool.Spawn();

            view.Setup(itemPm);
            view.SetDragContainer(_dragTransform);
            view.SetParent(_holder);

            _itemViews.Add(view);
            OnViewSpawned.Invoke(view);
        }

        private void Clear()
        {
            foreach (var inventoryItemView in _itemViews)
            {
                _pool.Unspawn(inventoryItemView);
                OnViewDespawned.Invoke(inventoryItemView);
            }
            _itemViews.Clear();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var view = eventData.pointerDrag.GetComponent<InventoryItemView>();
            if (view != null)
            {
                _signalBusService.Fire(new SetItemToInventory(view.Id));
            }
        }
    }
}
