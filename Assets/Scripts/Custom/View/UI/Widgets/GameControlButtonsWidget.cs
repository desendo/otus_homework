
using Custom.DependencyInjection;
using Custom.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    public class GameControlButtonsWidget : MonoBehaviour
    {
        [SerializeField] private Button _save;
        [SerializeField] private Button _load;
        [SerializeField] private Button _reset;

        [Inject]
        public void Construct(SignalBusService signalBusService)
        {
            _save.onClick.AddListener(()=> signalBusService.Fire(new SaveRequest()));
            _load.onClick.AddListener(()=> signalBusService.Fire(new LoadRequest()));
            _reset.onClick.AddListener(()=> signalBusService.Fire(new ResetRequest()));
        }
    }
}
