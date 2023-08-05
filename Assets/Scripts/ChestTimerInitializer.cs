using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Config;
using DependencyInjection;
using UnityEngine;

public class ChestTimerInitializer : MonoBehaviour
{
    [SerializeField] private List<ChestTimer> _timers;

    [Inject]
    public void Construct(GameConfig gameConfig, VisualConfig visualConfig)
    {
        var offlineTime = 0f;
        if (PlayerPrefs.HasKey("SavedTime"))
        {
            var time = DateTime.Parse(PlayerPrefs.GetString("SavedTime"), CultureInfo.InvariantCulture);
            offlineTime = (float)(DateTime.Now - time).TotalSeconds;
        }
        foreach (var chestTimer in _timers)
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
                chestTimer.AddTime(offlineTime);

            }



            if (spriteEntry != null)
            {
                chestTimer.SetupIcon(spriteEntry.Sprite);
            }

        }
    }

}