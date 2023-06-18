using Homework.Signals;

public class SaveDebugButton : DebugButton
{
    protected override void OnClick()
    {
        _signalBusService.Fire(new GameSignals.SaveRequest());
    }
}