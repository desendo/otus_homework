using System.Collections.Generic;
using System.Linq;
using DependencyInjection;
using UnityEngine;

namespace ItemInventory.UI
{
    public class ItemSlotsView : MonoBehaviour
    {
        [SerializeField] private List<SlotView> _slotViews;
        [SerializeField] private Transform _dragContainer;
        private HeroItemSlotsPresentationModel _pm;

        [Inject]
        public void Construct(HeroItemSlotsPresentationModel pm)
        {
            _pm = pm;
            pm.OnChange.Subscribe(OnUpdate);
            OnUpdate();
        }

        private void OnUpdate()
        {
            foreach (var slotView in _slotViews)
            {
                slotView.Clear();
            }

            foreach (var slotsPm in _pm.SlotsPms)
            {
                var view = _slotViews.FirstOrDefault(x => x.Type == slotsPm.SlotType);
                if (view != null)
                {
                    view.Setup(slotsPm);
                }
            }
        }
    }
}