using System;
using Specifications.Builds.Buildings;

namespace Dialogs.BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogModel
    {
        public event Action<string> OnLimitUpdated; 

        public BuildingSpecification Specification { get; private set; }
        
        public BuildCardDialogModel(BuildingSpecification specification)
        {
            Specification = specification;
        }

        public void RedrawLimitText(int currentLimit)
        {
            OnLimitUpdated?.Invoke(currentLimit.ToString());
        }
    }
}