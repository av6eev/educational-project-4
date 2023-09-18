using System;
using System.Collections.Generic;
using Specifications.Base;
using Specifications.Builds.Buildings;

namespace Specifications.Builds.BuildsCategory
{
    [Serializable]
    public class BuildsCategorySpecification : ISpecification
    {
        public string Category;
        public List<BuildingSpecificationSo> Buildings;
    }
}