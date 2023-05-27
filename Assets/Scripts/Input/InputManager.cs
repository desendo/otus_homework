using UnityEngine;

namespace Input
{
    public sealed class InputManager : IStartGame, IFinishGame, IUpdate
    {
        private bool _gameStarted;
        public float HorizontalDirection { get; private set; }
        public bool Fire { get; private set; }

        public void Update(float dt)
        {
            if(!_gameStarted)
                return;

            Fire = UnityEngine.Input.GetKeyDown(KeyCode.Space);

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                HorizontalDirection = -1;
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                HorizontalDirection = 1;
            else
                HorizontalDirection = 0;
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