using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Custom.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config/New Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        public List<string> Stats = new List<string>();
        public List<string> Avatars = new List<string>();

    }
}