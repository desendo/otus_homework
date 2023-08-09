using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;

namespace ItemInventory.UI
{
    public class ItemSlotsView : MonoBehaviour
    {
        [SerializeField] private List<SlotView> _slotViews;

        [Inject]
        public void Construct()
        {
        }
    }
}