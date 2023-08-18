namespace ItemInventory.Components
{
    public class ItemComponent_Weapon
    {
        public string WeaponId { get; }

        public ItemComponent_Weapon(string weaponId)
        {
            WeaponId = weaponId;
        }
    }
}