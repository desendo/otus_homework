using System;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class HeroModel : IDisposable
    {
        public readonly HeroModelCore Core = new HeroModelCore();
        public readonly HeroModelVisual Visual = new HeroModelVisual();

        public void Construct(Transform rootTransform, Animator animator, IUpdateProvider updateProvider, Rigidbody rigidbody)
        {
            Core.Construct();
            Visual.Construct(Core, rootTransform, animator, updateProvider, rigidbody);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}