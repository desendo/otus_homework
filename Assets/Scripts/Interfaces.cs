public interface IStartGameListener
{
    public void OnStartGame();
}
public interface IGameLoadedListener
{
    public void OnGameLoaded(bool isLoaded);
}
public interface IWinGameListener
{
    public void OnWinGame();
}
public interface ILostGameListener
{
    public void OnLostGame();
}

public interface IUpdate
{
    public void Update(float dt);
}
public interface ILateUpdate
{
    public void LateUpdate();
}
public interface IFixedUpdate
{
    public void FixedUpdate(float dt);
}
public interface IInit
{
    public void Init();
}
public interface ISpawner
{
    void Spawn();
    void Clear();
}