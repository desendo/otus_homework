using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class Entity : IEntity
    {
        private readonly List<object> components = new List<object>();

        public T Get<T>()
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                var component = this.components[i];
                if (component is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Component of type {typeof(T).Name} is not found!");
        }

        public bool TryGet<T>(out T result)
        {
            for (int i = 0, count = this.components.Count; i < count; i++)
            {
                var component = this.components[i];
                if (component is T tComponent)
                {
                    result = tComponent;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public void Add(object component)
        {
            this.components.Add(component);
        }

        public void Remove(object component)
        {
            this.components.Remove(component);
        }

        public virtual void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}