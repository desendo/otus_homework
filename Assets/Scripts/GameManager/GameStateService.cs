using ReactiveExtension;

namespace GameManager
{
    public sealed class GameStateService
    {
        private readonly Reactive<bool> _gameStartedReactive = new Reactive<bool>();
        public IReadonlyReactive<bool> GameStartedReactive => _gameStartedReactive;
        public readonly Event OnGameStart = new Event();
        public readonly Event OnGameFinish = new Event();

        public void SetGameStarted()
        {
            _gameStartedReactive.Value = true;
            OnGameStart?.Invoke();
        }
        public void SetGameFinished()
        {
            _gameStartedReactive.Value = false;
            OnGameFinish?.Invoke();
        }
    }
}