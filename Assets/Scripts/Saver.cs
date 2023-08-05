using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Saver : MonoBehaviour
{
    [SerializeField] private List<ChestTimer> _timers;

    private float _timer;

    private void LateUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = 1f;
            PlayerPrefs.SetString("SavedTime", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            foreach (var chestTimer in _timers)
            {
                PlayerPrefs.SetFloat(chestTimer.ChestId, chestTimer.Left);
            }
            PlayerPrefs.Save();
        }
    }
}