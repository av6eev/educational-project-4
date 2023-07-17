using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using Descriptions.Builds.BuildsCategory;

namespace BuildingsStatistic
{
    public class BuildingsStatisticModel
    {
        public event Action<string> OnLimitUpdated; 
        
        public readonly List<BuildingModel> Buildings = new();
        public readonly Dictionary<string, int> BuildingLimits = new();

        public BuildingsStatisticModel(List<BuildsCategoryDescription> buildsCategory)
        {
            foreach (var building in buildsCategory.SelectMany(category => category.Buildings))
            {
                BuildingLimits.Add(building.Description.Id, building.Description.Limit);
            }
        }

        public void UpdateLimit(string buildingId)
        {
            BuildingLimits[buildingId] -= BuildingLimits[buildingId] == 0 ? 0 : 1;
            OnLimitUpdated?.Invoke(buildingId);
        }
    }
}