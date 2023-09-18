using System;
using Utilities;

namespace Rewards.Base
{
    [Serializable]
    public abstract class BaseReward : IReward
    {
        public abstract void Give(GameManager manager);
    }
}