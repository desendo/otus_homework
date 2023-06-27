using Common.Entities;
using Models.Components;
using Models.Declarative;
using UnityEngine;

namespace Models.Entities
{
    public class BulletEntityMono : EntityMono
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private Transform _collisionTransform;

        private readonly BulletModelCore BulletModelCore = new BulletModelCore();
        private void Awake()
        {
            BulletModelCore.Construct(_collisionTransform, _lifeTime);

            Add(new Component_Collision(this, BulletModelCore.OnCollisionEntered));
            Add(new Component_Direction(BulletModelCore.Direction));
            Add(new Component_Move(BulletModelCore.Direction, BulletModelCore.Speed));
            Add(new Component_Finish(this, BulletModelCore.OnLifeTimeFinished, BulletModelCore.Lifetime));
            Add(new Component_Activate(BulletModelCore.OnActivate));
            Add(new Component_Damage(BulletModelCore.Damage));
            Add(new Component_Speed(BulletModelCore.Speed));
            Add(new Component_Transform(transform));
        }
    }
}