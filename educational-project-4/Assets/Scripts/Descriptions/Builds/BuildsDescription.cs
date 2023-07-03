using System;
using System.Collections.Generic;
using Descriptions.Base;
using Descriptions.Builds.BuildsCategory;

namespace Descriptions.Builds
{
    [Serializable]
    public class BuildsDescription : IDescription
    {
        public List<BuildsCategoryDescriptionSo> Categories;
    }
}