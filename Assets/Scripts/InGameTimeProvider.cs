using System.Collections.Generic;
using UnityEngine;

public class InGameTimeProvider : MonoBehaviour
{
    [SerializeField] private List<ChestTimer> _timers;

    private void Update()
    {
        foreach (var chestTimer in _timers)
        {
            if(chestTimer.IsSetUp)
                chestTimer.AddTime(Time.deltaTime);
        }
    }
}