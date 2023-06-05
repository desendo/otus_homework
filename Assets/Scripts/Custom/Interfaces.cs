namespace Custom
{
    public interface IDataLoadHandler<in T>
    {
        void SaveToData(T data);
        void LoadFromData(T data);
    }

    public interface IStartGame
    {
        public void StartGame();
    }
    public interface IFinishGame
    {
        public void FinishGame();
    }

    public interface IUpdate
    {
        public void Update(float dt);
    }
    public interface IFixedUpdate
    {
        public void FixedUpdate(float dt);
    }
    public interface IInit
    {
        public void Init();
    }
    public interface IReset
    {
        public void Reset();
    }
}