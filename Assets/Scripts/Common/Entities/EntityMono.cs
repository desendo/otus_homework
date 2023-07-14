using UnityEngine;

namespace Common.Entities
{
    public class EntityMono : MonoBehaviour, IEntity
    {
        private readonly Entity _entity = new Entity();
        public virtual T Get<T>()
        {
            return _entity.Get<T>();
        }

        public virtual bool TryGet<T>(out T result)
        {
            return _entity.TryGet<T>(out result);
        }

        public virtual void Add(object component)
        {
            _entity.Add(component);
        }

        public virtual void Remove(object component)
        {
            _entity.Remove(component);
        }

        public virtual void Dispose()
        {
        }
    }
}