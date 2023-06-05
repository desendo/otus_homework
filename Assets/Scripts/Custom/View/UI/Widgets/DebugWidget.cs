
using Custom.DependencyInjection;
using Custom.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    public class DebugWidget : MonoBehaviour
    {
        [SerializeField] private Button _add1;
        [SerializeField] private Button _add10;
        [SerializeField] private Button _add30;
        [SerializeField] private Button _addStat;

        [Inject]
        public void Construct(SignalBusService signalBusService)
        {
            _add1.onClick.AddListener(()=> signalBusService.Fire(new AddDebugExpRequest(10)));
            _add10.onClick.AddListener(()=> signalBusService.Fire(new AddDebugExpRequest(100)));
            _add30.onClick.AddListener(()=> signalBusService.Fire(new AddDebugExpRequest(300)));
            _addStat.onClick.AddListener(()=> signalBusService.Fire(new AddRandomStatRequest()));
        }
    }
}
