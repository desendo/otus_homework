using System;
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
        public EcsPackedEntity PackedEntity { get; private set; }
        public event Action<EntityMono, Collision> OnEntityCollision;
        public void OnCollisionEnter(Collision other)
        {
            OnEntityCollision?.Invoke(this, other);
        }


        public void SetEntity(EcsPackedEntity packedEntity)
        {
            PackedEntity = packedEntity;
        }

        public virtual void SetTeam(int team)
        {
            if (team == 1)
                _renderer.material = _team1;
            if (team == 0)
                _renderer.material = _team0;
        }
        public void SetDisposeCallback(Action action)
        {
            _disposeAction = action;
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