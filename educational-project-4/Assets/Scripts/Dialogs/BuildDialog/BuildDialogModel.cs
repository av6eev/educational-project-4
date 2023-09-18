using System;
using System.Collections.Generic;
using Dialogs.Base;
using Dialogs.BuildDialog.BuildCategoryDialog;
using Specifications.Base;

namespace Dialogs.BuildDialog
{
    public class BuildDialogModel : IDialogModel
    {
        public event Action OnShow, OnHide;
        public ISpecification Specification { get; }

        public List<BuildCategoryDialogModel> CategoriesModels { get; private set; } = new();
        
        public BuildDialogModel(ISpecification specification)
        {
            Specification = specification;
        }
        
        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }
    }
}