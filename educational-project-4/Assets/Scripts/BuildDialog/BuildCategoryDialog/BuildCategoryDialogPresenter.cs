using System.Collections.Generic;
using System.Linq;
using BuildDialog.BuildCategoryDialog.BuildCardDialog;
using Descriptions.Builds.BuildsCategory.Buildings;
using Earth;
using Utilities;

namespace BuildDialog.BuildCategoryDialog
{
    public class BuildCategoryDialogPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildCategoryDialogModel _model;
        private readonly BuildCategoryDialogView _view;
        
        private readonly PresentersEngine _presenters = new();

        public BuildCategoryDialogPresenter(GameManager manager, BuildCategoryDialogModel model, BuildCategoryDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.Down -= OnClick;
            _model.OnDataClear -= ClearData;
        }

        public void Activate()
        {
            _view.Down += OnClick;
            _model.OnDataClear += ClearData;
        }

        private void ClearData()
        {
            _view.ClearBuildingCards();
            
            _presenters.Deactivate();
            _presenters.Clear();
        }

        private void OnClick()
        {
            var categoriesModels = _manager.BuildDialogModel.CategoriesModels;
            
            if (categoriesModels.Any(category => category.IsActive && _model == category)) return;            
            
            foreach (var model in categoriesModels.FindAll(item => item.Description.Category != _model.Description.Category))
            {
                model.IsActive = false;
                model.ClearData();
            }
            
            _model.IsActive = true;
            
            var sortedDescriptions = _model.Description.Buildings.OrderBy(item => item.Description.Priority).ToArray();

            foreach (var description in sortedDescriptions)
            {
                var remainingLimit = _manager.StatisticModel.BuildingLimits[description.Description.Id];
                var model = new BuildCardDialogModel(description.Description);
                var view = _view.InstantiateBuildingCard(_manager.CoreStartView.BuildDialogView.BuildingContentRoot, model.Description.Title, remainingLimit.ToString(), model.Description.PreviewImage);

                if (remainingLimit == 0)
                {
                    view.enabled = false;
                }

                var presenter = new BuildCardDialogPresenter(_manager, model, view);
                
                _model.CardsModels.Add(model);
                _presenters.Add(presenter);
            }
            
            _model.Description.Buildings = new List<BuildingDescriptionSo>(sortedDescriptions);
            
            _presenters.Activate();
            _manager.CoreStartView.BuildDialogView.BuildingRoot.SetActive(true);
        }
    }
}