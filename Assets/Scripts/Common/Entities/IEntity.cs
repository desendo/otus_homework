using System;

namespace Common.Entities
{
    public interface IEntity : IDisposable
    {
        T Get<T>();

        bool TryGet<T>(out T element);
        public void Add(object component);
        public void Remove(object component);

    }
}