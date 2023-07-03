using System.Collections.Generic;
using BuildDialog.BuildCategoryDialog;
using Descriptions.Builds;

namespace BuildDialog
{
    public class BuildDialogModel
    {
        public readonly BuildsDescription Description;
        public readonly List<BuildCategoryDialogModel> CategoriesModels = new();
        
        public BuildDialogModel(BuildsDescription description)
        {
            Description = description;
        }
    }
}