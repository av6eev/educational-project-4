using Dialogs.BuildDialog.BuildCategoryDialog;
using Dialogs.ChangeFloorDialog;
using Utilities;

namespace Dialogs.BuildDialog
{
    public class BuildDialogPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildDialogModel _model;
        private readonly BuildDialogView _view;

        private readonly PresentersEngine _presenters = new();

        private int _toggleCounter;

        public BuildDialogPresenter(GameManager manager, BuildDialogModel model, BuildDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.ToggleButton.onClick.RemoveListener(OnToggle);
        }

        public void Activate()
        {
            _view.ToggleButton.onClick.AddListener(OnToggle);
        }

        private void OnToggle()
        {
            HandleDialog(_toggleCounter % 2 == 0);
            
            _toggleCounter++;
        }

        private void HandleDialog(bool state)
        {
            switch (state)
            {
                case true:
                    _manager.DialogsModel.GetByType<ChangeFloorDialogModel>().Hide();
                    
                    foreach (var description in _manager.Descriptions.BuildsCategory)
                    {
                        var model = new BuildCategoryDialogModel(description);
                        var presenter = new BuildCategoryDialogPresenter(_manager, model, _view.InstantiateCategoryView(model.Description.Category));
                    
                        _model.CategoriesModels.Add(model);
                        _presenters.Add(presenter);
                    }
            
                    _presenters.Activate();
                    break;
                case false:
                    _view.ClearCategories();
                    
                    var activeCategory = _model.CategoriesModels.Find(item => item.IsActive);
                    activeCategory?.ClearData();
                    
                    _model.CategoriesModels.Clear();
                    _presenters.Deactivate();
                    _presenters.Clear();
                    
                    _manager.DialogsModel.GetByType<ChangeFloorDialogModel>().Show();
                    break;
            }
            
            _view.CategoriesRoot.SetActive(state);
            _view.BuildingRoot.SetActive(state);
        }
    }
}