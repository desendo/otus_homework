using System;
using System.Collections.Generic;
using System.Linq;
using ItemInventory.Components;
using UnityEngine;

namespace ItemInventory.Config
{
    [CreateAssetMenu(menuName = "Create ItemEffectConfig", fileName = "ItemEffectConfig", order = 0)]
    public class ItemEffectConfig : PropertyConfig
    {
        [SerializeField] private Effect[] _effects;
        public override object CreateComponent()
        {
            return new ItemComponent_Effect(_effects.ToList());
        }
    }

    [Serializable]
    public class Effect
    {
        public EffectType Type;
        public EffectApplyType ApplyType;
        public string Value;
    }

    public enum EffectType
    {
        None = 0,
        Evasion = 1,
        WeaponDamageMult = 2,
        ArmorDamageSub = 3,
        MaxHealthAdd = 4,
        MoveSpeedMult = 5
    }
    public enum EffectApplyType
    {
        None = 0,
        Inventory = 1,
        Slot = 2,
        Use = 3,
    }
}