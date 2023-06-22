namespace Common.Entities
{
    public interface IEntity
    {
        T Get<T>();

        bool TryGet<T>(out T element);
        public void Add(object component);
        public void Remove(object component);


    }
}