namespace Homework
{
    public interface ISaveDataProvider
    {
        public bool TryGetData<T>(out T data);
        public void SetData<T>(T data);
    }
}