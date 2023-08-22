using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using ItemInventory.Components;
using ItemInventory.Config;
using Services;
using Services.Effects;
using Signals;
using UnityEngine;

namespace ItemInventory.Controllers
{
    public class SlotItemEffectController: IStartGameListener, IFinishGameListener
    {
        private readonly HeroSlotsService _heroSlotsService;
        private readonly Inventory _itemInventory;
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private EffectManager _effectManager;

        public SlotItemEffectController(HeroSlotsService heroSlotsService, Inventory itemInventory,
            SignalBusService signalBusService, EffectManager effectManager)
        {
            _heroSlotsService = heroSlotsService;
            _itemInventory = itemInventory;
            _effectManager = effectManager;
        }


        public void OnStartGame()
        {


            foreach (var slot in _heroSlotsService.Slots)
                SubscribeToSLot(slot);

            _heroSlotsService.OnSlotAdd.Subscribe(SubscribeToSLot).AddTo(_subs);

            foreach (var item in _heroSlotsService.Items)
            {
                if (item.TryGet<ItemComponent_Effect>(out var effectComponent))
                    ApplyEffect(effectComponent);
            }
        }

        private void SubscribeToSLot(ItemSlot slot)
        {
            slot.OnEquip.Subscribe(item =>
            {
                if (item.TryGet<ItemComponent_Effect>(out var effectComponent))
                    ApplyEffect(effectComponent);
            }).AddTo(_subs);
            slot.OnUnEquip.Subscribe(item =>
            {
                if (item.TryGet<ItemComponent_Effect>(out var effectComponent))
                    RemoveEffect(effectComponent);
            }).AddTo(_subs);
        }


        public void OnFinishGame(bool gameWin)
        {
            _subs.Dispose();
        }
        private void ApplyEffect(ItemComponent_Effect effectComponent)
        {
            foreach (var e in effectComponent.Effects)
            {
                if (e.ApplyType == EffectApplyType.Slot)
                {
                    _effectManager.AddEffect(e);
                }
            }

        }
        private void RemoveEffect(ItemComponent_Effect effectComponent)
        {
            foreach (var e in effectComponent.Effects)
            {
                if (e.ApplyType == EffectApplyType.Slot)
                {
                    _effectManager.RemoveEffect(e);
                }
                
            }
        }
    }
}