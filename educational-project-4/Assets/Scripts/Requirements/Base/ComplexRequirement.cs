using Utilities;

namespace Requirements.Base
{
    public abstract class ComplexRequirement : IRequirement
    {
        public abstract bool Completed { get; set; }

        public RequirementType Type { get; } = RequirementType.Complex;
        public abstract SubRequirementType SubType { get; }

        public abstract string RewardName { get; protected set; }
        public abstract string SideRewardName { get; protected set; }
        public abstract bool Check(GameManager manager);
    }
}