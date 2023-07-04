using System;
using Common;
using GameManager;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    public class EnemyModel : IDisposable
    {
        public readonly EnemyModelCore Core = new EnemyModelCore();
        public readonly EnemyModelVisual Visual = new EnemyModelVisual();

        public void Construct(Transform rootTransform, Animator animator, IUpdateProvider updateProvider,
            Rigidbody rigidbody, Collider hitCollider, AnimationEventListener animationEventListener,
            CollisionSensor weaponCollisionSensor)
        {
            Core.Construct(updateProvider);
            Visual.Construct(Core, animator, updateProvider, rigidbody, hitCollider, animationEventListener, weaponCollisionSensor);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}