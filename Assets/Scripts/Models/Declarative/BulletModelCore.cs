using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    [Serializable]
    public class BulletModelCore : MonoBehaviour
    {
        [SerializeField] private CollisionSensor _collisionSensor;
        [SerializeField] private float _lifeTime;
        public readonly AtomicVariable<Vector3> Direction = new AtomicVariable<Vector3>();
        public readonly AtomicVariable<float> Damage = new AtomicVariable<float>();
        public readonly AtomicVariable<float> Lifetime = new AtomicVariable<float>();
        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public readonly AtomicEvent OnActivate = new AtomicEvent();
        public readonly AtomicEvent<Collision> OnCollisionEntered = new AtomicEvent<Collision>();
        public readonly AtomicEvent OnLifeTimeFinished = new AtomicEvent();
        private float _life;

        public void Construct()
        {
            _collisionSensor.Collision
                .Subscribe(collision => OnCollisionEntered.Invoke(collision));
            Lifetime.OnChanged.Subscribe(left =>
            {
                if(left <= 0 )
                    OnLifeTimeFinished.Invoke();
            });
            OnActivate.Subscribe(() => Lifetime.Value = _lifeTime);
        }
    }
}