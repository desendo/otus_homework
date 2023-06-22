using Common.Entities;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Config/New VisualConfig")]
    public sealed class VisualConfig : ScriptableObject
    {
        public EntityMono PlayerPrefab;
    }
}