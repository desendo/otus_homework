using System;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class HeroModel : IDisposable
    {
        public readonly HeroModelCore Core = new HeroModelCore();
        public readonly HeroModelVisual Visual = new HeroModelVisual();

        public void Construct(Transform rootTransform, Animator animator, IUpdateProvider updateProvider)
        {
            Core.Construct();
            Visual.Construct(Core, rootTransform, animator, updateProvider);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}