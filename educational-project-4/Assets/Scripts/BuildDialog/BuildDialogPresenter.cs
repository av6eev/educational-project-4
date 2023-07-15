using BuildDialog.BuildCategoryDialog;
using Earth;
using Utilities;

namespace BuildDialog
{
    public class BuildDialogPresenter : IPresenter
    {
        private readonly EarthLocationManager _manager;
        private readonly BuildDialogModel _model;
        private readonly BuildDialogView _view;

        private readonly PresentersEngine _presenters = new();

        private int _toggleCounter;

        public BuildDialogPresenter(EarthLocationManager manager, BuildDialogModel model, BuildDialogView view)
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
                    break;
            }
            
            _view.CategoriesRoot.SetActive(state);
            _view.BuildingRoot.SetActive(state);
        }
    }
}