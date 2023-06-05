using Custom.Signals;
using Custom.View;
using Custom.View.PresentationModel;
using Custom.View.UI.Popup;

namespace Custom.Controllers
{
    public class OpenLevelUpPopupController
    {
        public OpenLevelUpPopupController(SignalBusService signalBus, LevelUpPresentationModel levelUpPresentationModel,
            IViewService viewService)
        {
            signalBus.Subscribe<OpenLevelUpPopupRequest>(x =>
            {
                viewService.Show<LevelUpPopup>(levelUpPresentationModel);
            });
        }
    }
}