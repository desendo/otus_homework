using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Config/New VisualConfig")]
    public sealed class VisualConfig : ScriptableObject
    {
        public List<SpriteEntry> SpriteEntries;
    }
    [Serializable]
    public class SpriteEntry
    {
        public string Id;
        public Sprite Sprite;
    }
}