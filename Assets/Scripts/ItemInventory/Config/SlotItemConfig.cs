using ItemInventory.Components;
using UnityEngine;

namespace ItemInventory.Config
{
    [CreateAssetMenu(menuName = "Create SlotItemConfig", fileName = "SlotItemConfig", order = 0)]
    public class SlotItemConfig : PropertyConfig
    {
        [SerializeField] private SlotType _slotType;
        public override object CreateComponent()
        {
            return new ItemComponent_SlotType(_slotType);
        }
    }
}