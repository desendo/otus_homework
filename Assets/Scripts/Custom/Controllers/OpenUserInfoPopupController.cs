using Custom.Signals;
using Custom.View;
using Custom.View.PresentationModel;
using Custom.View.UI.Popup;

namespace Custom.Controllers
{
    public class OpenUserInfoPopupController
    {
        public OpenUserInfoPopupController(SignalBusService signalBus, IUserInfoPresentationModel userInfoPresentationModel,
            IViewService viewService)
        {
            signalBus.Subscribe<OpenUserInfoPopupRequest>(x =>
            {
                viewService.Show<UserInfoPopup>(userInfoPresentationModel);
            });
        }
    }
}