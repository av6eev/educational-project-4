using Utilities;

namespace Requirements.Base
{
    public interface IRequirement
    {
        bool Completed { get; set; }
        RequirementType Type { get; }
        SubRequirementType SubType { get; }
        bool Check(GameManager manager);
    }
}