using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;
        public bool IsPlayer => _isPlayer;
    }
}