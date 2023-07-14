using System;
using Common;
using GameManager;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    [Serializable]
    public class EnemyModel : MonoBehaviour, IDisposable
    {
        public EnemyModelCore Core = new EnemyModelCore();
        [SerializeField]
        public EnemyModelVisual Visual;

        public void Construct(IUpdateProvider updateProvider)
        {
            Core.Construct(updateProvider);
            Visual.Construct(Core, updateProvider);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}