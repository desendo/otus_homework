public interface IStartGameListener
{
    public void OnStartGame();
}
public interface IGameLoadedListener
{
    public void OnGameLoaded(bool isLoaded);
}

public interface IFinishGameListener
{
    public void OnFinishGame(bool gameWin);
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
