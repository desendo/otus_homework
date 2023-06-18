namespace Homework
{
    public interface ISaveLoader
    {
        bool TryLoad(ISaveDataProvider saveDataProvider);
        void Save(ISaveDataProvider saveDataProvider);
    }
}