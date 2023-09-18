using System.Collections.Generic;
using System.Linq;
using Dialogs.BuildDialog.BuildCategoryDialog.BuildCardDialog;
using Specifications.Builds.Buildings;
using Utilities;

namespace Dialogs.BuildDialog.BuildCategoryDialog
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
            _view.OnMouseDown -= OnClick;
            _model.OnDataClear -= ClearData;
            
            ClearData();
        }

        public void Activate()
        {
            _view.OnMouseDown += OnClick;
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
            var categoriesModels = _manager.DialogsModel.GetByType<BuildDialogModel>().CategoriesModels;
            
            if (categoriesModels.Any(category => category.IsActive && _model == category)) return;            
            
            foreach (var model in categoriesModels.FindAll(item => item.Specification.Category != _model.Specification.Category))
            {
                model.IsActive = false;
                model.ClearData();
            }
            
            _model.IsActive = true;
            
            var sortedSpecifications = _model.Specification.Buildings.OrderBy(item => item.Specification.Priority).ToArray();

            foreach (var specification in sortedSpecifications)
            {
                var remainingLimit = _manager.StatisticModel.BuildingLimits[specification.Specification.Id];
                var model = new BuildCardDialogModel(specification.Specification);
                var view = _view.InstantiateBuildingCard(_manager.CoreStartView.DialogsView.BuildDialogView.BuildingContentRoot, model.Specification.Title, remainingLimit.ToString(), model.Specification.PreviewImage);

                if (remainingLimit == 0)
                {
                    view.enabled = false;
                }

                var presenter = new BuildCardDialogPresenter(_manager, model, view);
                
                _model.CardsModels.Add(model);
                _presenters.Add(presenter);
            }
            
            _model.Specification.Buildings = new List<BuildingSpecificationSo>(sortedSpecifications);
            
            _presenters.Activate();
            _manager.CoreStartView.DialogsView.BuildDialogView.BuildingRoot.SetActive(true);
        }
    }
}