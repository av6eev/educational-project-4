using System.Collections.Generic;
using UnityEngine;

namespace Specifications.Requirements
{
    [CreateAssetMenu(menuName = "Create Specifications Collection/New RequirementsData Specification", fileName = "RequirementsDataSpecification", order = 51)]
    public class RequirementsDataAsset : ScriptableObject
    {
        public List<RequirementAsset> Assets;
    }
}