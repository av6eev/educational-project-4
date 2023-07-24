using Descriptions.Builds.BuildsCategory.Buildings;

namespace Building.Dialog
{
    public class BuildingDialogModel
    {
        public BuildingDescription Description;
        
        public BuildingDialogModel(BuildingDescription description)
        {
            Description = description;
        }
    }
}