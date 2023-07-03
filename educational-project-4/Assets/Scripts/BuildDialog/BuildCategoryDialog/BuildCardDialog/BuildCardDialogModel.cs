using System;
using Descriptions.Builds.BuildsCategory.Buildings;

namespace BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogModel
    {
        public event Action<string> OnLimitUpdated; 

        public readonly BuildingDescription Description;
        
        public BuildCardDialogModel(BuildingDescription description)
        {
            Description = description;
        }

        public void RedrawLimitText(int currentLimit)
        {
            OnLimitUpdated?.Invoke(currentLimit.ToString());
        }
    }
}