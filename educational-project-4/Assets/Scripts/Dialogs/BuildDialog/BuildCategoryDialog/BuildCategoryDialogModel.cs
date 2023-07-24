using System;
using System.Collections.Generic;
using Descriptions.Builds.BuildsCategory;
using Dialogs.BuildDialog.BuildCategoryDialog.BuildCardDialog;

namespace Dialogs.BuildDialog.BuildCategoryDialog
{
    public class BuildCategoryDialogModel
    {
        public event Action OnDataClear;

        public readonly BuildsCategoryDescription Description;
        
        public readonly List<BuildCardDialogModel> CardsModels = new();

        public bool IsActive;

        public BuildCategoryDialogModel(BuildsCategoryDescription description)
        {
            Description = description;
        }

        public void ClearData()
        {
            OnDataClear?.Invoke();   
        }
    }
}