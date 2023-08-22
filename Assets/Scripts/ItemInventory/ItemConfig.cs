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

    public enum SlotType
    {
        None = 0,
        Hand = 1,
        Legs = 2,
        Body = 4,
        Amulet = 5,
        Ring = 6,

    }

}