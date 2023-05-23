public interface IGameListener
{
}
public interface IGameStartListener : IGameListener
{
    void OnGameStart();
}
public interface IGameReadyListener : IGameListener
{
    void OnGameReady();
}
public interface IGameFinishListener : IGameListener
{
    void OnGameFinish();
}
public interface IPauseListener : IGameListener
{
    void OnPaused(bool isPaused);
}
public interface ITick : IGameListener
{
    void Tick(float dt);
}