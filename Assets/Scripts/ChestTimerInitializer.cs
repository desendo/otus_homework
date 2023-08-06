using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Config;
using DependencyInjection;
using UnityEngine;

public class ChestTimerInitializer
{
    [Inject]
    public void Construct(GameConfig gameConfig, VisualConfig visualConfig,  List<ChestTimer> timers)
    {

        var offlineDeltaTime = 0f;
        if (PlayerPrefs.HasKey("SavedTime"))
        {
            var time = DateTime.Parse(PlayerPrefs.GetString("SavedTime"), CultureInfo.InvariantCulture);
            offlineDeltaTime = (float)(DateTime.Now - time).TotalSeconds;
        }
        foreach (var chestTimer in timers)
        {
            chestTimer.gameObject.SetActive(false);
            var config = gameConfig.ChestConfigs.FirstOrDefault(x => x.Id == chestTimer.ChestId);
            var spriteEntry = visualConfig.SpriteEntries.FirstOrDefault(x => x.Id == chestTimer.ChestId);
            if (config != null)
            {
                chestTimer.gameObject.SetActive(true);
                var left = config.Time;
                if (PlayerPrefs.HasKey(config.Id))
                    left = PlayerPrefs.GetFloat(config.Id);
                chestTimer.SetupTimer(config.Time, left);
                chestTimer.AddTime(offlineDeltaTime);
            }

            if (spriteEntry != null)
            {
                chestTimer.SetupIcon(spriteEntry.Sprite);
            }

        }
    }

}