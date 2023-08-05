using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        public List<RewardConfig> Rewards;
        public List<ChestConfig> ChestConfigs;

    }

    [System.Serializable]
    public class RewardConfig
    {
        public string Id;
        public RewardType Type;
        public int Value;
    }

    public enum RewardType
    {
        None = 0,
        Gold = 1,
        Gems = 2,
        Wood = 3,
        Cache = 4,
        BuildBoost = 5,

    }

    [System.Serializable]
    public class ChestConfig
    {
        public string Id;
        public string Name;
        public List<RewardIdWeight> RewardIdWeights;

        public int MaxRewards;
        public int MinRewards;
        public float Time;

        [System.Serializable]
        public class RewardIdWeight
        {
            public string RewardId;
            public int Weight;
        }
    }

}