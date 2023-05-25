using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Input
{
    public sealed class InputManager : IStartGame, IFinishGame, IFixedUpdate, IUpdate
    {
        private readonly Character.Character _character;
        private readonly CharacterController _characterController;
        private bool _gameStarted = false;
        private float HorizontalDirection { get; set; }
        public InputManager(Character.Character character, CharacterController characterController)
        {
            _character = character;
            _characterController = characterController;
        }

        public void Update(float dt)
        {
            if(!_gameStarted)
                return;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) _characterController._fireRequired = true;

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                HorizontalDirection = -1;
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                HorizontalDirection = 1;
            else
                HorizontalDirection = 0;
        }

        public void FixedUpdate(float dt)
        {
            if(!_gameStarted)
                return;

            _character.MoveComponent
                .MoveByRigidbodyVelocity(new Vector2(HorizontalDirection, 0) * dt);
        }

        public void StartGame()
        {
            _gameStarted = true;
        }

        public void FinishGame()
        {
            _gameStarted = false;
        }
    }
}