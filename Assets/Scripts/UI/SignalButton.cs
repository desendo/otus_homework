using DependencyInjection;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SignalButton<T> : MonoBehaviour where T : new()
    {
        private bool _constructed;

        [Inject]
        public void Construct(SignalBusService signalBusService)
        {
            if(_constructed)
                return;

            _constructed = true;
            GetComponent<Button>().onClick.AddListener(()=>signalBusService.Fire(new T()));
        }
    }
}