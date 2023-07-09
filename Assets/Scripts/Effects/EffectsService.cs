using UnityEngine;

namespace Effects
{
    public class EffectsService
    {
        private readonly HitEffectPool _hitEffectPool;
        private readonly TeleportationEffectPool _teleportationEffectPool;
        private readonly ExplosionEffectPool _explosionEffectPool;

        public EffectsService(HitEffectPool hitEffectPool, TeleportationEffectPool teleportationEffectPool, ExplosionEffectPool explosionEffectPool)
        {
            _teleportationEffectPool = teleportationEffectPool;
            _explosionEffectPool = explosionEffectPool;
            _hitEffectPool = hitEffectPool;
        }
        public void ShowTeleportEffect(Vector3 point, Vector3 normal)
        {
            var effectInstance = _teleportationEffectPool.Spawn();
            effectInstance.transform.position = point;
            effectInstance.transform.LookAt(point + normal);
        }
        public void ShowHitEffect(Vector3 point, Vector3 normal)
        {
            var effectInstance = _hitEffectPool.Spawn();
            effectInstance.transform.position = point;
            effectInstance.transform.LookAt(point + normal);
        }
        public void ShowExplosionEffect(Vector3 point)
        {
            var effectInstance = _explosionEffectPool.Spawn();
            effectInstance.transform.position = point;
        }
        public void ShowHitEffect(Collision collision)
        {
            var midPoint = Vector3.zero;
            var midNormal = Vector3.zero;
            foreach (var contactPoint2D in collision.contacts)
            {
                midPoint += contactPoint2D.point;
                midNormal += contactPoint2D.normal;
            }

            midNormal /= collision.contacts.Length;
            midPoint /= collision.contacts.Length;
            ShowHitEffect(midPoint, midNormal);
        }
        public void OnStartGame()
        {
            _hitEffectPool.Clear();
        }
    }
}