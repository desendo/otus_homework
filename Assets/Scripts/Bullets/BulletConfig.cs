using Common;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] public PhysicsLayer _physicsLayer;
        [SerializeField] public Color _color;
        [SerializeField] public int _damage;
        [SerializeField] public float _speed;
    }
}