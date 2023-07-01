using UnityEngine;

namespace Effects
{
    public class EffectsService : IStartGameListener
    {
        private readonly HitEffectPool _hitEffectPool;

        public EffectsService(HitEffectPool hitEffectPool)
        {
            _hitEffectPool = hitEffectPool;
        }

        public void ShowHitEffect(Vector3 point, Vector3 normal)
        {
            var effectInstance = _hitEffectPool.Spawn();
            effectInstance.transform.position = point;
            effectInstance.transform.LookAt(point + normal);
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