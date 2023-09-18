using System.Collections.Generic;
using Dialogs.BuildDialog.BuildCategoryDialog;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.BuildDialog
{
    public class BuildDialogView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform CategoriesContentRoot { get; private set; }
        [field: SerializeField] public GameObject CategoriesRoot { get; private set; }
        [field: SerializeField] public BuildCategoryDialogView CategoryCellPrefab { get; private set; }
        
        [field: SerializeField] public RectTransform BuildingContentRoot { get; private set; }
        [field: SerializeField] public GameObject BuildingRoot { get; private set; }
        
        [field: SerializeField] public Button ToggleButton { get; private set; }

        private readonly List<BuildCategoryDialogView> _categories = new();

        public BuildCategoryDialogView InstantiateCategoryView(string title)
        {
            var view = Instantiate(CategoryCellPrefab, CategoriesContentRoot);
            view.TitleTxt.text = title;
            
            _categories.Add(view);
            return view;
        }
        
        public void ClearCategories()
        {
            foreach (var view in _categories)
            {
                Destroy(view.gameObject);
            }
            
            _categories.Clear();
        }
    }
}