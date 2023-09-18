using Utilities;

namespace Requirements.Base
{
    public abstract class SingleRequirement : IRequirement
    {
        public abstract bool Completed { get; set; }
        public RequirementType Type { get; } = RequirementType.Single;
        public abstract SubRequirementType SubType { get; }
        public abstract string RewardName { get; protected set; }
        public abstract bool Check(GameManager manager);
    }
}