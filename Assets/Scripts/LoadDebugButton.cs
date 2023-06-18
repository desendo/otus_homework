using Homework.Signals;

public class LoadDebugButton : DebugButton
{
    protected override void OnClick()
    {
        _signalBusService.Fire(new GameSignals.LoadRequest());
    }
}