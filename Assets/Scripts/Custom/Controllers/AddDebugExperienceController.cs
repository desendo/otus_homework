using Custom.Signals;
using Custom.View;

namespace Custom.Controllers
{
    public class AddDebugExperienceController
    {
        private readonly IViewService _viewService;

        public AddDebugExperienceController(SignalBusService signalBus, PlayerLevel playerLevel)
        {
            signalBus.Subscribe<AddDebugExpRequest>(x =>
            {
                playerLevel.AddExperience(x.Exp);
            });
        }
    }
}