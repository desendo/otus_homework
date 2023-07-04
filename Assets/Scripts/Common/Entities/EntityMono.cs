using UnityEngine;

namespace Common.Entities
{
    public class EntityMono : MonoBehaviour, IEntity
    {
        private readonly Entity _entity = new Entity();
        public T Get<T>()
        {
            return _entity.Get<T>();
        }

        public bool TryGet<T>(out T result)
        {
            return _entity.TryGet<T>(out result);
        }

        public void Add(object component)
        {
            _entity.Add(component);
        }

        public void Remove(object component)
        {
            _entity.Remove(component);
        }

        public virtual void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}