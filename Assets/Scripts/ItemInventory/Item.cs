using System.Collections.Generic;
using Common.Entities;
using UnityEngine;

namespace ItemInventory
{
    public class Item : Entity
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }

        public Item(string id, string name, string description, Sprite icon, List<PropertyConfig> scriptableObjects)
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            scriptableObjects?.ForEach(x=> Add(x.CreateComponent()));
        }
    }
}