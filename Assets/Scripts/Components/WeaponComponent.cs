using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;

        public Vector2 Position => _firePoint.position;

        public Quaternion Rotation => _firePoint.rotation;
    }
}