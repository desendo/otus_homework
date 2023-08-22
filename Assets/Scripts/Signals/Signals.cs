using System;
using ItemInventory;

namespace Signals
{
    internal struct GameOverRequest
    {
    }
    internal readonly struct SetItemToSlotRequest
    {
        public string SlotId { get; }
        public string ItemId { get; }

        public SetItemToSlotRequest(string slotId, string itemId)
        {
            SlotId = slotId;
            ItemId = itemId;
        }
    }
    internal readonly struct SetItemToInventory
    {
        public string ItemId { get; }

        public SetItemToInventory( string itemId)
        {
            ItemId = itemId;
        }
    }
    internal struct GameWinRequest
    {
    }

    public struct GameStartRequest
    {
    }

}