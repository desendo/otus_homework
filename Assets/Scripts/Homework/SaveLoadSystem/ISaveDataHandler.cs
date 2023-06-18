namespace Homework
{
    public interface ISaveDataHandler
    {

        public void SetData<T>(T data);
        public object GetData<T>();
        public void SaveData();
        public void LoadData();
    }
}