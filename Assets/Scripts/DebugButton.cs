
using System;
using DependencyInjection;
using Homework.Signals;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
   protected SignalBusService _signalBusService;

   [Inject]
   public void Construct(SignalBusService signalBusService)
   {
      _signalBusService = signalBusService;
   }

   private void Awake()
   {
      GetComponent<Button>().onClick.AddListener(OnClick);
   }

   protected virtual void OnClick()
   {
      
   }
}
