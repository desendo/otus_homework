using System;
using Common;
using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Views
{
    public class EntityMono : MonoBehaviour
    {
        [SerializeField] private Material _team1;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _team0;
        private Action _disposeAction;
        public EcsPackedEntityWithWorld PackedEntity { get; private set; }
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<EntityMono>(out var otherEntityMono))
            {
                if (otherEntityMono.PackedEntity.Unpack(out var world, out _))
                {
                    var hitEntity = world.NewEntity();

                    var hitPool = world.GetPool<CHit>();
                    var positionPool = world.GetPool<CPosition>();

                    ref var hit = ref hitPool.Add(hitEntity);
                    ref var hitPoint = ref positionPool.Add(hitEntity);

                    var midPoint = Vector3.zero;
                    var midNormal = Vector3.zero;
                    foreach (var contactPoint2D in other.contacts)
                    {
                        midPoint += contactPoint2D.point;
                        midNormal += contactPoint2D.normal;
                    }

                    midNormal /= other.contacts.Length;
                    midPoint /= other.contacts.Length;

                    hitPoint.Position = midPoint;
                    hitPoint.Direction = midNormal;
                    hit.EntityFirst = PackedEntity;
                    hit.EntitySecond = otherEntityMono.PackedEntity;
                }
            }
        }
        public void SetEntity(EcsPackedEntityWithWorld packedEntity)
        {
            PackedEntity = packedEntity;
        }

        public void SetDisposeAction(Action action)
        {
            _disposeAction = action;
        }

        public virtual void SetTeam(int team)
        {
            if (team == 1)
                _renderer.material = _team1;
            if (team == 0)
                _renderer.material = _team0;
        }
        protected void SetLayer(int layer)
        {
            gameObject.layer = layer;
        }

        public void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}