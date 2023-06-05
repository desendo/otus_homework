using Custom.DependencyInjection;
using Custom.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.View.UI.Widgets
{
    public class OpenPopupsButtonsWidget : MonoBehaviour
    {
        [SerializeField] private Button _openLevel;
        [SerializeField] private Button _openPlayer;

        [Inject]
        public void Construct(SignalBusService signalBusService)
        {
            _openLevel.onClick.AddListener(()=> signalBusService.Fire(new OpenLevelUpPopupRequest()));
            _openPlayer.onClick.AddListener(()=> signalBusService.Fire(new OpenUserInfoPopupRequest()));
        }
    }
}
