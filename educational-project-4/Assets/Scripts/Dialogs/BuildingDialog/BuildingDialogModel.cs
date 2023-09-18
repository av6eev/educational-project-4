using Specifications.Builds.Buildings;

namespace Dialogs.BuildingDialog
{
    public class BuildingDialogModel
    {
        public BuildingSpecification Specification;
        
        public BuildingDialogModel(BuildingSpecification specification)
        {
            Specification = specification;
        }
    }
}