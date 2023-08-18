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
        public List<PropertyConfig> Parameters;

        public Item CreateItem()
        {
            var item = new Item(Id, Name, Description, Sprite, Parameters);
            
            return item;
        }

    }

    public abstract class PropertyConfig : ScriptableObject
    {
        public abstract object CreateComponent();
    }


    [Flags]
    public enum EffectFlags
    {
        None = 0,
        Wear = 1,
        Inventory = 2,

    }
    public enum SlotType
    {
        None = 0,
        Hand = 1,
        Legs = 2,
        Body = 4,
        Amulet = 5,
        Ring = 6,

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