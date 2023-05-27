using System;
using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        private BulletConfig _bulletConfig;

        public Vector2 Position => _firePoint.position;
        public BulletConfig BulletConfig
        {
            get
            {
                if (_bulletConfig == null)
                    throw new Exception($"Cant get config. run {nameof(SetBulletConfig)} first");

                return _bulletConfig;
            }
        }

        public Quaternion Rotation => _firePoint.rotation;

        public void SetBulletConfig(BulletConfig bulletConfig)
        {
            _bulletConfig = bulletConfig;
        }
    }
}