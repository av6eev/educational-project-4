using Rewards.Base;
using UnityEngine;

namespace Specifications.Rewards
{
    public abstract class RewardAssetSo : ScriptableObject
    {
        public abstract BaseReward Get();
    }
}