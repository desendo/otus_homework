using System.Collections.Generic;
using UnityEngine;


namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        public float ZombieMoveSpeed;
        public float ZombieHitDelay;
        public int ZombieHitDamage;
        public List<PlayerWeapon> PlayerWeapons;
        public int PlayerHealth;
        public int PlayerMoveSpeed;
        public int PlayerRotationSpeed;
        public float PlayerReloadTime;
        public int PlayerClipSize;
        public float PlayerShotDelay;
        public int PlayerDamage;
    }

    [System.Serializable]
    public class PlayerWeapon
    {
        public string Id;
        public WeaponType Type;
        public List<Parameter> Parameters;
    }

    public enum WeaponType
    {
        None = 0,
        Sword = 1,
        Riffle = 2,
        Shotgun = 3,
        MachineGun = 4,
        Flamer = 5,
        RocketLauncher = 6,
        GrenadeLauncher = 7,
        Laser = 8,
    }
    [System.Serializable]

    public class Parameter
    {
        public string Id;
        public string Value;
    }
}