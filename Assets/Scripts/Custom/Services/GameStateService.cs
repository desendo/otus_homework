using Custom.ReactiveExtension;

namespace Custom.Services
{
    public sealed class GameStateService
    {
        private readonly Reactive<bool> _gameStartedReactive = new Reactive<bool>();
        private readonly Reactive<bool> _gameLoadedReactive = new Reactive<bool>();
        public IReadonlyReactive<bool> GameStartedReactive => _gameStartedReactive;
        public IReadonlyReactive<bool> GameLoadedReactive => _gameStartedReactive;
        public void SetGameLoaded(bool isLoaded)
        {
            _gameLoadedReactive.Value = isLoaded;
        }
    }
}