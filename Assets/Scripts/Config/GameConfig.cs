using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        public List<PlayerWeapon> Weapons;
        public int PlayerHealth;
        public float PlayerMoveSpeed;
        public int PlayerRotationSpeed;
        public float ZombieSpawnInterval;
        public int KillGoal;
        public int ZombieHealth;
        public float ZombieMoveSpeed;

    }

    [System.Serializable]
    public class PlayerWeapon
    {
        public string Id;
        public WeaponType Type;
        public List<Parameter> Parameters;
    }
    [System.Serializable]

    public class Parameter
    {
        public string Id;
        public string Value;
    }
    public enum WeaponType
    {
        None = 0,
        Riffle = 2,
        Shotgun = 3,
        MachineGun = 4,
        Flamer = 5,
        RocketLauncher = 6,
        GrenadeLauncher = 7,
        Laser = 8,
        ZombieHands = 9,
    }

}