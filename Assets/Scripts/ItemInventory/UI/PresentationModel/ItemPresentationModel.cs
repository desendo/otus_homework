using ItemInventory;
using ReactiveExtension;
using UnityEngine;

namespace UI.PresentationModel
{
    public class ItemPresentationModel
    {
        public Reactive<string> Name = new Reactive<string>();
        public Reactive<string> Count = new Reactive<string>();
        public Reactive<string> Description = new Reactive<string>();
        public Reactive<Sprite> Icon = new Reactive<Sprite>();
        public Item Item { get; }
        public string Id { get; private set; }

        public ItemPresentationModel(Item item)
        {
            Icon.Value = item.Icon;
            Count.Value = "";
            Description.Value = item.Description;
            Name.Value = item.Name;
            Item = item;
            Id = item.Id;
        }
    }
}