public interface IGameStartListener
{
    void OnGameStart();
}
public interface IGameEndListener
{
    void OnGameEnd();
}
public interface IPauseListener
{
    void OnPaused(bool isPaused);
}
public interface ITick
{
    void Tick(float dt);
}