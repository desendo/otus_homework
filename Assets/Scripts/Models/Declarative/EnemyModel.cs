using System;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class EnemyModel : IDisposable
    {
        public readonly EnemyModelCore Core = new EnemyModelCore();
        public readonly EnemyModelVisual Visual = new EnemyModelVisual();

        public void Construct(Transform rootTransform, Animator animator, IUpdateProvider updateProvider,
            Rigidbody rigidbody, Collider hitCollider)
        {
            Core.Construct(updateProvider);
            Visual.Construct(Core, rootTransform, animator, updateProvider, rigidbody, hitCollider);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}