using System;
using ItemInventory;

namespace Signals
{
    internal struct GameOverRequest
    {
    }
    internal readonly struct SetItemToSlotRequest
    {
        public ItemSlot Slot { get; }
        public Action Success { get; }
        public Action Fail { get; }



        public SetItemToSlotRequest(ItemSlot slot, Action success, Action fail)
        {
            Slot = slot;
            Success = success;
            Fail = fail;
        }
    }
    internal struct GameWinRequest
    {
    }

    public struct GameStartRequest
    {
    }

}