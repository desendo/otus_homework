using System.Collections.Generic;
using UnityEngine;

namespace Custom.Config
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "VisualConfig/New VisualConfig")]
    public sealed class VisualConfig : ScriptableObject
    {
        public List<SpriteEntry> SpriteEntries = new List<SpriteEntry>();
    }
}