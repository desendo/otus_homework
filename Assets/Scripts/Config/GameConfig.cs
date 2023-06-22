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

    }

    [System.Serializable]
    public class PlayerWeapon
    {
        public WeaponType Type;
        public List<Parameter> Parameters;
    }

    public enum WeaponType
    {
        None = 0,
        Sword = 1,
        Pistol = 2,
        Shotgun = 3,
    }
    [System.Serializable]

    public class Parameter
    {
        public string Id;
        public string Value;
    }
}