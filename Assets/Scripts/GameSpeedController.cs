public class GameSpeedController : IGameReadyListener, IGameFinishListener
{
    private readonly PlayerController _playerController;
    private readonly WorldManager _worldManager;

    public GameSpeedController(PlayerController playerController, WorldManager worldManager)
    {
        _playerController = playerController;
        _worldManager = worldManager;
    }

    public void OnGameReady()
    {
        _playerController.OnScore += OnScore;
        OnScore(_playerController.Score);
    }
    public void OnGameFinish()
    {
        _playerController.OnScore -= OnScore;
    }
    private void OnScore(int score)
    {
        _worldManager.SetMoveFactor(1f + score / 10f);
    }


}