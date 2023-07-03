using System;
using System.Collections.Generic;
using BuildDialog.BuildCategoryDialog.BuildCardDialog;
using Descriptions.Builds.BuildsCategory;
using Descriptions.Builds.BuildsCategory.Buildings;

namespace BuildDialog.BuildCategoryDialog
{
    public class BuildCategoryDialogModel
    {
        public event Action OnDataClear;
        public event Action<BuildingDescription> OnCardsResort; 

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