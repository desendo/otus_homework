using UnityEngine;

namespace Character
{
    public class CharacterService : IStartGame
    {
        private Vector3 _initialPosition;
        public GameObject Character { get; private set; }

        public void SetCharacter(GameObject character)
        {
            Character = character;
            _initialPosition = character.transform.position;
        }

        public void StartGame()
        {
            Character.transform.position = _initialPosition;
        }
    }
}