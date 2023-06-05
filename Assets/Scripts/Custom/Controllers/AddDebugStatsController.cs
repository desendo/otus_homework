using System;
using System.Linq;
using Custom.Config;
using Custom.Signals;
using Custom.View;
using UnityEngine;

namespace Custom.Controllers
{
    public class AddDebugStatsController
    {
        private readonly IViewService _viewService;
        private IDisposable _openSup;

        public AddDebugStatsController(SignalBusService signalBus, CharacterInfo characterInfo, GameConfig gameConfig)
        {
            signalBus.Subscribe<AddRandomStatRequest>(x =>
            {
                var stats = characterInfo.GetStats();
                var freeConfig = gameConfig.Stats.FirstOrDefault(statId => stats.All(stat => stat.Name != statId));
                if(freeConfig != null)
                    characterInfo.AddStat(new CharacterStat(freeConfig, 1));
                else
                {
                    Debug.Log("all possible stats taken. see: GameConfig");
                }
            });
        }
    }
}