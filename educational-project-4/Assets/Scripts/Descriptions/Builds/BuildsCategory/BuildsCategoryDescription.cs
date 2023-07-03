using System;
using System.Collections.Generic;
using Descriptions.Base;
using Descriptions.Builds.BuildsCategory.Buildings;

namespace Descriptions.Builds.BuildsCategory
{
    [Serializable]
    public class BuildsCategoryDescription : IDescription
    {
        public string Category;
        public List<BuildingDescriptionSo> Buildings;
    }
}