using ItemInventory.Components;
using UnityEngine;

namespace ItemInventory.Config
{
    [CreateAssetMenu(menuName = "Create WeaponItemConfig", fileName = "WeaponItemConfig", order = 0)]
    public class WeaponItemConfig : PropertyConfig
    {
        [SerializeField] private string _weaponId;
        public override object CreateComponent()
        {
            return new ItemComponent_Weapon(_weaponId);
        }
    }
}