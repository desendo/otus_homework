
using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemInventory.UI
{

    public class SlotView : MonoBehaviour, IDropHandler
    {
        [SerializeField] private SlotType _slotType;

        private InventoryItemView _currentItemView;
        private SlotPresentationModel _pm;

        public SlotType Type => _slotType;

        public bool IsSetUp { get; set; }

        public void Setup(SlotPresentationModel pm)
        {
            _pm = pm;
            IsSetUp = true;
        }


        public void Clear()
        {
            IsSetUp = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var view = eventData.pointerDrag.GetComponent<InventoryItemView>();
            if (view != null)
            {
                _pm.DropRequest(() =>
                {
                    var rectTransform = view.GetComponent<RectTransform>();
                    rectTransform.SetParent(transform);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    //rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                    rectTransform.anchoredPosition = Vector2.zero;
                    view.SetIsInSlot(true);

                }, () =>
                {
                    view.SetIsInSlot(false);
                });
            }

        }
    }
}
