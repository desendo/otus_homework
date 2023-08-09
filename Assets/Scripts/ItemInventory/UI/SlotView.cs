using System;
using System.Collections.Generic;
using Config;
using DependencyInjection;
using Pool;
using UI.PresentationModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemInventory.UI
{
    public enum SlotType
    {
        Any = 0,
        Hand  = 1,
        Body = 2,
        Ring = 3,
        Amulet = 4,
        Legs = 5
    }

    public class SlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryItemView _currentItemView;

        [SerializeField] private SlotType _slotType;

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }
    }
}
