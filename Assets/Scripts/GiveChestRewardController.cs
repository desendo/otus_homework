using System.Collections.Generic;
using System.Linq;
using Config;
using DependencyInjection;
using UnityEngine;

public class GiveChestRewardController
{

    private GameConfig _gameConfig;
    private List<ChestTimer> _timers;

    [Inject]
    public void Construct(GameConfig gameConfig, List<ChestTimer> timers)
    {
        _timers = timers;
        _gameConfig = gameConfig;
        foreach (var chestTimer in _timers)
        {
            chestTimer.OnClick += ProcessClick;
        }
    }


    private void ProcessClick(ChestTimer chestTimer)
    {
        if (chestTimer.IsSetUp && chestTimer.RewardReady)
        {
            var rewardIdsToGive = new List<string>();

            var config = _gameConfig.ChestConfigs.FirstOrDefault(x => x.Id == chestTimer.ChestId);

            if (config != null)
            {
                var rewardCount = Random.Range(config.MinRewards, config.MaxRewards + 1);
                var rewardsLeft = config.RewardIdWeights.ToList();
                for (int i = 0; i < rewardCount; i++)
                {
                    if (rewardsLeft.Count > 0)
                    {
                        float totalWeight = rewardsLeft.Sum(x => x.Weight);
                        var randomWeight = Random.value * totalWeight;
                        var sum = 0;
                        foreach (var reward in rewardsLeft)
                        {
                            sum += reward.Weight;
                            if (!(sum >= randomWeight)) continue;

                            rewardsLeft.Remove(reward);
                            rewardIdsToGive.Add(reward.RewardId);
                            break;

                        }
                    }
                }
            }

            GiveRewards(rewardIdsToGive);

            chestTimer.ResetTimer();
        }
    }

    private void GiveRewards(List<string> rewardIdsToGive)
    {
        foreach (var s in rewardIdsToGive)
        {
            Debug.Log("give reward "+s);
        }
    }
}