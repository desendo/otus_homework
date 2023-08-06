using System;
using System.Collections.Generic;
using System.Globalization;
using DependencyInjection;
using UnityEngine;

public class Saver : IUpdate
{
    private List<ChestTimer> _timers;

    private float _timer;

    [Inject]
    public void Construct(List<ChestTimer> timers)
    {
        _timers = timers;
    }

    public void Update(float dt)
    {
        _timer -= dt;
        if (_timer <= 0)
        {
            _timer = 1f;
            PlayerPrefs.SetString("SavedTime", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            foreach (var chestTimer in _timers)
            {
                PlayerPrefs.SetFloat(chestTimer.ChestId, chestTimer.TimeLeft);
            }
            PlayerPrefs.Save();
        }
    }
}