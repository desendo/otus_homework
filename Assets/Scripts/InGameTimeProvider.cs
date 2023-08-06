using System.Collections.Generic;
using DependencyInjection;


public class InGameTimeProvider : IUpdate
{
    private List<ChestTimer> _timers;

    [Inject]
    public void Construct(List<ChestTimer> timers)
    {
        _timers = timers;
    }

    public void Update(float dt)
    {
        foreach (var chestTimer in _timers)
        {
            if(chestTimer.IsSetUp)
                chestTimer.AddTime(dt);
        }
    }

    public void Init()
    {

    }
}