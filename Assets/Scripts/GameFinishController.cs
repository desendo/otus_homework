public class GameFinishController
{
    public GameFinishController(GameStateService gameStateService, PlayerController playerController)
    {
        playerController.OnHit += gameStateService.FinishGame;
    }
}