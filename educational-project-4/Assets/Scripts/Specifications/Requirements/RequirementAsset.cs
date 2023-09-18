using Requirements.Base;
using UnityEngine;

namespace Specifications.Requirements
{
    public abstract class RequirementAsset : ScriptableObject
    {
        public abstract IRequirement Get();
    }
}