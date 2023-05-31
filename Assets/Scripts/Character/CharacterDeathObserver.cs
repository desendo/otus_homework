using Components;
using Effects;
using GameManager;
using UnityEngine;

namespace Character
{
    public sealed class CharacterDeathObserver : IStartGame, IFinishGame
    {
        private readonly CharacterService _characterService;
        private readonly EffectsService _effectsService;
        private readonly GameStateService _gameStateService;
        private bool _started;

        public CharacterDeathObserver(CharacterService characterService, EffectsService effectsService, GameStateService gameStateService)
        {
            _characterService = characterService;
            _effectsService = effectsService;
            _gameStateService = gameStateService;
        }

        public void StartGame()
        {
            _characterService.Character.GetComponent<HitPointsComponent>().HpEmpty += OnCharacterDeath;
        }

        public void FinishGame()
        {
            _characterService.Character.GetComponent<HitPointsComponent>().HpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject x)
        {
            _effectsService.ShowExplosionEffect(x.transform.position);
            x.SetActive(false);
            _gameStateService.SetGameFinished();
        }
    }
}