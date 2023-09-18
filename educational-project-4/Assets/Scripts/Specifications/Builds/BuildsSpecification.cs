using System;
using System.Collections.Generic;
using Specifications.Base;
using Specifications.Builds.BuildsCategory;

namespace Specifications.Builds
{
    [Serializable]
    public class BuildsSpecification : ISpecification
    {
        public List<BuildsCategorySpecificationSo> Categories;
    }
}