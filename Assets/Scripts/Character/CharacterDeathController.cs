using Components;
using GameManager;
using UnityEngine;

namespace Character
{
    public sealed class CharacterDeathController : IStartGame, IFinishGame
    {
        private readonly CharacterMono _characterMono;
        private readonly GameStateService _gameStateService;
        private bool _started;

        public CharacterDeathController(CharacterMono characterMono, GameStateService gameStateService)
        {
            _characterMono = characterMono;
            _gameStateService = gameStateService;
        }

        public void StartGame()
        {
            _characterMono.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        public void FinishGame()
        {
            _characterMono.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject x)
        {
            _gameStateService.SetGameFinished();
        }
    }
}