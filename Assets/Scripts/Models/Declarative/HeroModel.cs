using System;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class HeroModel : MonoBehaviour, IDisposable
    {
        [SerializeField] public HeroModelVisual Visual;
        public readonly HeroModelCore Core = new HeroModelCore();

        public void Construct(IUpdateProvider updateProvider)
        {
            Core.Construct();
            Visual.Construct(Core, updateProvider);
        }

        public void Dispose()
        {
            Core.Dispose();
            Visual.Dispose();
        }
    }
}