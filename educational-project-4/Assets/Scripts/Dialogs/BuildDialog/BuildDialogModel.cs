using System;
using System.Collections.Generic;
using Descriptions.Base;
using Dialogs.Base;
using Dialogs.BuildDialog.BuildCategoryDialog;

namespace Dialogs.BuildDialog
{
    public class BuildDialogModel : IDialogModel
    {
        public event Action OnShow, OnHide;
        public IDescription Description { get; }

        public readonly List<BuildCategoryDialogModel> CategoriesModels = new();
        
        public BuildDialogModel(IDescription description)
        {
            Description = description;
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