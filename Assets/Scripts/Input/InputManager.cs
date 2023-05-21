using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : IStartGame, IFinishGame, IFixedUpdate, IUpdate
    {
        private readonly Character _character;
        private readonly CharacterController _characterController;
        private bool _gameStarted = false;
        private float HorizontalDirection { get; set; }
        public InputManager(Character character, CharacterController characterController)
        {
            _character = character;
            _characterController = characterController;
        }

        public void Update(float dt)
        {
            if(!_gameStarted)
                return;

            if (Input.GetKeyDown(KeyCode.Space)) _characterController._fireRequired = true;

            if (Input.GetKey(KeyCode.LeftArrow))
                HorizontalDirection = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
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