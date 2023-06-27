using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    public class BulletModelCore
    {
        public readonly AtomicVariable<Vector3> Direction = new AtomicVariable<Vector3>();
        public readonly AtomicVariable<int> Damage = new AtomicVariable<int>();
        public readonly AtomicVariable<float> Lifetime = new AtomicVariable<float>();
        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public readonly AtomicEvent OnActivate = new AtomicEvent();
        public readonly AtomicEvent<Collision> OnCollisionEntered = new AtomicEvent<Collision>();
        public readonly AtomicEvent OnLifeTimeFinished = new AtomicEvent();
        private float _life;

        public void Construct(Transform collisionTransform, float lifeTime)
        {
            _life = lifeTime;
            collisionTransform.gameObject.GetComponent<CollisionSensor>().Collision
                .Subscribe(collision => OnCollisionEntered.Invoke(collision));
            Lifetime.OnChanged.Subscribe(left =>
            {
                if(left <= 0 )
                    OnLifeTimeFinished.Invoke();
            });
            OnActivate.Subscribe(() => Lifetime.Value = _life);
        }
    }
}