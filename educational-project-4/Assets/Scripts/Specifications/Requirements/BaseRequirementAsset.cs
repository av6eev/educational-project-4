using Requirements.Base;

namespace Specifications.Requirements
{
    public class BaseRequirementAsset<T> : RequirementAsset where T : IRequirement
    {
        public T Requirement;
        public override IRequirement Get() => Requirement;
    }
}