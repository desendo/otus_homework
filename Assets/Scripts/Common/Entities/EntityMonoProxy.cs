using UnityEngine;

namespace Common.Entities
{
    public sealed class EntityMonoProxy : EntityMono
    {
        [SerializeField]
        public EntityMono _entity;

        public override T Get<T>()
        {
            return _entity.Get<T>();
        }

        public override bool TryGet<T>(out T element)
        {
            return _entity.TryGet(out element);
        }
    }
}