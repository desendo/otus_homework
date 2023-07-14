using Common.Entities;
using Models.Components;
using Models.Declarative;
using UnityEngine;

namespace Models.Entities
{
    public class BulletEntityMono : EntityMono
    {
        [SerializeField] private BulletModelCore _bulletModelCore;
        private void Awake()
        {
            _bulletModelCore.Construct();

            Add(new Component_Collision(this, _bulletModelCore.OnCollisionEntered));
            Add(new Component_Direction(_bulletModelCore.Direction));
            Add(new Component_Move(_bulletModelCore.Direction, _bulletModelCore.Speed));
            Add(new Component_Finish(this, _bulletModelCore.OnLifeTimeFinished, _bulletModelCore.Lifetime));
            Add(new Component_Activate(_bulletModelCore.OnActivate));
            Add(new Component_Damage(_bulletModelCore.Damage));
            Add(new Component_Speed(_bulletModelCore.Speed));
            Add(new Component_Transform(transform));
        }
    }
}