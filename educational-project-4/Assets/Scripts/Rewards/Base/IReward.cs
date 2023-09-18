using Utilities;

namespace Rewards.Base
{
    public interface IReward
    {
        void Give(GameManager manager);
    }
}