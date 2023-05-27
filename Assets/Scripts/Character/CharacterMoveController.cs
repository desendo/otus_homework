using Components;
using Config;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class CharacterMoveController : IStartGame, IFinishGame, IFixedUpdate
    {
        private readonly InputManager _inputManager;
        private readonly CharacterMono _characterMono;
        private bool _started;
        private readonly GameConfig _gameConfig;

        public CharacterMoveController(InputManager inputManager, CharacterMono characterMono, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _inputManager = inputManager;
            _characterMono = characterMono;
        }

        public void StartGame()
        {
            _started = true;
        }

        public void FinishGame()
        {
            _started = false;
        }

        public void FixedUpdate(float dt)
        {
            if(!_started)
                return;

            var velocity = new Vector2(_inputManager.HorizontalDirection, 0) * (dt * _gameConfig.CharMoveSpeed);
            _characterMono.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(velocity);

        }
    }
}