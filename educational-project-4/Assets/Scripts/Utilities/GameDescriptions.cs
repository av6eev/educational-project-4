using System.Collections.Generic;
using System.Linq;
using Descriptions.Base;
using Descriptions.Builds;
using Descriptions.Builds.BuildsCategory;
using Descriptions.Builds.BuildsCategory.Buildings;
using Descriptions.Dialogs.BuildingDialog;

namespace Utilities
{
    public class GameDescriptions
    {
        public readonly BuildsDescription Builds;

        public readonly List<BuildingDescription> Buildings = new();
        public readonly List<BuildsCategoryDescription> BuildsCategory = new();
        public readonly List<BuildingDialogDescription> BuildingDialogs = new();

        public GameDescriptions(DescriptionsCollectionSo collection)
        {
            Builds = collection.Collection.Builds.Description;
            
            foreach (var description in Builds.Categories.Select(element => element.Description))
            {
                BuildsCategory.Add(description);
            }
            
            foreach (var building in BuildsCategory.SelectMany(description => description.Buildings.Select(element => element.Description)))
            {
                Buildings.Add(building);
            }
            
            foreach (var buildingDialog in collection.Collection.BuildingDialogs.Select(description => description.Description))
            {
                BuildingDialogs.Add(buildingDialog);
            }
        }
    }
}