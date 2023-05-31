using UnityEngine;

namespace Effects
{
    public class EffectsService : IStartGame
    {
        private readonly HitEffectPool _hitEffectPool;
        private ExplosionEffectPool _explosionEffectPool;

        public EffectsService(HitEffectPool hitEffectPool, ExplosionEffectPool explosionEffectPool)
        {
            _hitEffectPool = hitEffectPool;
            _explosionEffectPool = explosionEffectPool;
        }

        public void ShowHitEffect(Vector2 point, Vector2 normal)
        {
            var effectInstance = _hitEffectPool.Spawn();
            effectInstance.transform.position = point;
            effectInstance.transform.LookAt(point + normal);
        }

        public void ShowExplosionEffect(Vector2 point)
        {
            var effectInstance = _explosionEffectPool.Spawn();
            effectInstance.transform.position = point;
            effectInstance.transform.LookAt(point);
        }

        public void StartGame()
        {
            _hitEffectPool.Clear();
            _explosionEffectPool.Clear();
        }
    }
}