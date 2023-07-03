using System.Collections.Generic;
using BuildDialog.BuildCategoryDialog;
using UnityEngine;
using UnityEngine.UI;

namespace BuildDialog
{
    public class BuildDialogView : MonoBehaviour
    {
        public RectTransform CategoriesContentRoot;
        public GameObject CategoriesRoot;
        public BuildCategoryDialogView CategoryCellPrefab;
        
        public RectTransform BuildingContentRoot;
        public GameObject BuildingRoot;
        
        public Button ToggleButton;

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