
using System;
using DependencyInjection;
using ItemInventory.UI.PresentationModel;
using Pool;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemInventory.UI
{

    public class SlotView : MonoBehaviour, IDropHandler
    {
        [SerializeField] private SlotType _slotType;
        [SerializeField] private Transform _dragContainer;
        [SerializeField] private string _id;
        private InventoryItemView _currentItemView;
        private SlotPresentationModel _pm;
        private SignalBusService _signalBusService;
        private InventoryItemViewPool _inventoryItemViewPool;
        private IDisposable _pmSub;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public SlotType Type => _slotType;


        [Inject]
        public void Construct(SignalBusService signalBusService, InventoryItemViewPool inventoryItemViewPool)
        {
            _inventoryItemViewPool = inventoryItemViewPool;
            _signalBusService = signalBusService;
        }

        public void Setup(SlotPresentationModel pm)
        {
            _pm = pm;
            Id = pm.Id;

            _pmSub?.Dispose();
            _pmSub = pm.OnChange.Subscribe(() => { OnChange(pm); });
            OnChange(pm);
        }

        private void OnChange(SlotPresentationModel pm)
        {
            if (_currentItemView != null)
            {
                _inventoryItemViewPool.Unspawn(_currentItemView);
                _currentItemView = null;
            }

            if (pm.CurrentItemPm == null)
            {
                return;
            }

            _currentItemView = _inventoryItemViewPool.Spawn();

            _currentItemView.Setup(pm.CurrentItemPm);
            _currentItemView.SetDragContainer(_dragContainer);
            PlaceView(_currentItemView);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var view = eventData.pointerDrag.GetComponent<InventoryItemView>();
            if (view != null)
            {
                _signalBusService.Fire(new SetItemToSlotRequest(Id, view.Id));
            }
        }

        private void PlaceView(InventoryItemView view)
        {
            var rectTransform = view.GetComponent<RectTransform>();
            view.SetParent(transform);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.offsetMin = new Vector2(0,0);
            rectTransform.offsetMax = new Vector2(0,0);
        }
    }
}
