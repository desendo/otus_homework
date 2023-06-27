using System;
using Common.Entities;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Config/New VisualConfig")]
    public sealed class VisualConfig : ScriptableObject
    {
        public EntityMono PlayerPrefab;
    }
    [Serializable]
    public class SpriteEntry
    {
        public string Id;
        public Sprite Sprite;
    }
}