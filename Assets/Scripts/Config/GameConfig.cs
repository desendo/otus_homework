
using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        public float SpawnInterval;
        public int SpawnBurstCount;
        public float Health;
        public EngineConfig EngineConfig;
        public WeaponConfig WeaponConfig;
    }

    [Serializable]
    public class EngineConfig
    {
        public float MaxMoveSpeed;
        public float MoveAcceleration;
    }
    [Serializable]
    public class WeaponConfig
    {
        public float Damage;
        public float Speed;
        public float Range;
        public float Angle;
        public float Delay;
        public float TimeToLive;
    }
}