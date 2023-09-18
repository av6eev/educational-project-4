using Rewards.Base;

namespace Specifications.Rewards
{
    public class BaseRewardAsset<T> : RewardAssetSo where T : BaseReward
    {
        public T Reward;
        public override BaseReward Get() => Reward;
    }
}