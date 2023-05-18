public class GameFinishController
{
    public GameFinishController(GameStateManager gameStateManager, PlayerController playerController)
    {
        playerController.OnHit += gameStateManager.FinishGame;
    }
}