using Components;
using Config;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class CharacterMoveController : IStartGame, IFinishGame, IFixedUpdate
    {
        private readonly InputManager _inputManager;
        private readonly CharacterService _characterService;
        private bool _started;
        private readonly GameConfig _gameConfig;

        public CharacterMoveController(InputManager inputManager, CharacterService characterService, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _inputManager = inputManager;
            _characterService = characterService;
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
            _characterService.Character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(velocity);

        }
    }
}