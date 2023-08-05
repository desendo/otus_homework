using System.Collections.Generic;
using System.Linq;
using Config;
using DependencyInjection;
using UnityEngine;

public class GiveChestRewardController : MonoBehaviour
{
    [SerializeField] private List<ChestTimer> _timers;
    private GameConfig _gameConfig;

    [Inject]
    public void Construct(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    private void Start()
    {
        foreach (var chestTimer in _timers)
        {
            chestTimer.OnClick += ChestTimer;
        }
    }

    private void ChestTimer(ChestTimer chestTimer)
    {
        if (chestTimer.IsSetUp && chestTimer.RewardReady)
        {
            List<string> rewardIdsToGive = new List<string>();

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
                        string rewardToAdd = null;
                        foreach (var reward in rewardsLeft)
                        {
                            sum += reward.Weight;
                            if (sum >= randomWeight)
                            {
                                rewardToAdd = reward.RewardId;
                                rewardsLeft.Remove(reward);
                                rewardIdsToGive.Add(rewardToAdd);
                                break;
                            }

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