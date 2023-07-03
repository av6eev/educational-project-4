using Descriptions.Builds.BuildsCategory.Buildings;

namespace BuildingDialog
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