using System;
using System.Collections.Generic;
using System.Linq;
using Building;
using Specifications.Builds.BuildsCategory;

namespace BuildingsStatistic
{
    public class BuildingsStatisticModel
    {
        // public event Action<string> OnLimitUpdated; 
        public event Action<string> OnBuildingRegistered;
        public List<BuildingModel> Buildings { get; } = new();
        public Dictionary<string, int> BuildingLimits { get; } = new();

        public BuildingsStatisticModel(List<BuildsCategorySpecification> buildsCategory)
        {
            foreach (var building in buildsCategory.SelectMany(category => category.Buildings))
            {
                BuildingLimits.Add(building.Specification.Id, building.Specification.Limit);
            }
        }

        public void UpdateLimit(string buildingId)
        {
            BuildingLimits[buildingId] -= BuildingLimits[buildingId] == 0 ? 0 : 1;
            // OnLimitUpdated?.Invoke(buildingId);
        }

        public void RegisterBuilding(BuildingModel model)
        {
            Buildings.Add(model);
            OnBuildingRegistered?.Invoke(model.Id);
        }
    }
}