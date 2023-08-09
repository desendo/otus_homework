using System;
using System.Collections.Generic;
using Config;
using UnityEngine;

namespace ItemInventory
{
    [CreateAssetMenu(menuName = "Create ItemConfig", fileName = "ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] public string Id;
        [SerializeField] public Sprite Sprite;
        [SerializeField] public string Name;
        [SerializeField] public string Description;
        [SerializeField] public InventoryItemFlags InventoryItemFlags;
        public List<ScriptableObject> Parameters;

    }

    
    public abstract class Container<T> : ScriptableObject
    {
        public List<T> List;
    }

    public class EffectsContainer : Container<EffectConfig>
    {
    }

    public class ItemPropertyConfig : ScriptableObject
    {

    }
    public class ItemSlotConfig : ScriptableObject
    {
        public SlotFlags SlotFlags;
    }
    public class EffectConfig : ScriptableObject
    {
        public string Id;
        public string Value;
        public EffectFlags ApplyCondition;
    }

    [Flags]
    public enum EffectFlags
    {
        None = 0,
        Wear = 1,
        Inventory = 2,

    }
    public enum SlotFlags
    {
        None = 0,
        Hand = 1,
        Legs = 2,
        Body = 4,

    }
    [Flags]
    public enum InventoryItemFlags
    {
        None = 0, //0
        Stackable = 1, //01
        Consumable = 2, //10
        CanEquip = 4, //100
        EquippedEffect = 8, //1000
        InventoryEffect = 16 //1000
    }
}