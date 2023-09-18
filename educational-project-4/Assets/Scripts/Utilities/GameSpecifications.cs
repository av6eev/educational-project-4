using System.Collections.Generic;
using System.Linq;
using Requirements.Base;
using Rewards.Base;
using Specifications.Base;
using Specifications.Builds;
using Specifications.Builds.Buildings;
using Specifications.Builds.BuildsCategory;
using Specifications.Dialogs.BuildingDialog;
using Specifications.Floors;
using Specifications.Requirements;

namespace Utilities
{
    public class GameSpecifications
    {
        public BuildsSpecification Builds { get; }
        public List<BuildingSpecification> Buildings { get; } = new();
        public List<BuildsCategorySpecification> BuildsCategory { get; } = new();
        public List<BuildingDialogSpecification> BuildingDialogs { get; } = new();
        public FloorsSpecification Floors { get; }
        public List<FloorSpecification> FloorsData { get; } = new();
        public Dictionary<string, IReward> Rewards { get; } = new();
        public Dictionary<string, IRequirement> Requirements { get; } = new();

        public GameSpecifications(SpecificationsCollectionSo collection)
        {
            Builds = collection.Collection.Builds.Specification;
            Floors = collection.Collection.Floors.Specification;
            
            foreach (var specification in Builds.Categories.Select(element => element.Specification))
            {
                BuildsCategory.Add(specification);
            }
            
            foreach (var building in BuildsCategory.SelectMany(specification => specification.Buildings.Select(element => element.Specification)))
            {
                Buildings.Add(building);
            }
            
            foreach (var buildingDialog in collection.Collection.BuildingDialogs.Select(specification => specification.Specification))
            {
                BuildingDialogs.Add(buildingDialog);
            }

            foreach (var floor in Floors.Floors.Select(specification => specification.Specification))
            {
                FloorsData.Add(floor);
            }
            
            foreach (var reward in collection.Collection.RewardsData.Assets)        
            {
                Rewards.Add(reward.name, reward.Get());
            }
            
            foreach (var requirement in collection.Collection.RequirementsData.Assets)
            {
                Requirements.Add(requirement.name, requirement.Get());
            }
        }
    }
}