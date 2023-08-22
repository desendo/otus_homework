using System.Collections.Generic;
using System.Linq;
using DependencyInjection;
using ItemInventory.UI.PresentationModel;
using UnityEngine;
using Event = ReactiveExtension.Event;

namespace ItemInventory.UI
{
    public class ItemSlotsView : MonoBehaviour
    {
        [SerializeField] private List<SlotView> _slotViews;
        private HeroItemSlotsPresentationModel _pm;
        private readonly List<SlotView> _views = new List<SlotView>();

        [Inject]
        public void Construct(HeroItemSlotsPresentationModel pm)
        {
            _pm = pm;
            pm.OnSlotPmAdd.Subscribe(OnAdd);
            pm.OnSlotPmRemoved.Subscribe(OnRemoved);
            OnUpdate();
        }

        private void OnRemoved(SlotPresentationModel obj)
        {
            var view = _views.FirstOrDefault(x => x.Id == obj.Id);
            if (view != null)
                _views.Remove(view);
        }

        private void OnAdd(SlotPresentationModel obj)
        {
            var view = _slotViews.FirstOrDefault(x => x.Type == obj.SlotType && !_views.Contains(x));
            if (view != null)
            {
                _views.Add(view);
                view.Setup(obj);
            }
        }

        private void OnUpdate()
        {
            foreach (var slotsPm in _pm.SlotsPms)
            {
                var view = _slotViews.FirstOrDefault(x => x.Type == slotsPm.SlotType && !_views.Contains(x));
                if (view != null)
                {
                    _views.Add(view);
                    view.Setup(slotsPm);
                }
            }
        }
    }
}