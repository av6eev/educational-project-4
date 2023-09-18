using System;
using System.Collections.Generic;
using Dialogs.BuildDialog.BuildCategoryDialog.BuildCardDialog;
using Specifications.Builds.BuildsCategory;

namespace Dialogs.BuildDialog.BuildCategoryDialog
{
    public class BuildCategoryDialogModel
    {
        public event Action OnDataClear;

        public BuildsCategorySpecification Specification { get; private set; }
        public List<BuildCardDialogModel> CardsModels { get; private set; } = new();

        public bool IsActive { get; set; }

        public BuildCategoryDialogModel(BuildsCategorySpecification specification)
        {
            Specification = specification;
        }

        public void ClearData()
        {
            OnDataClear?.Invoke();   
        }
    }
}