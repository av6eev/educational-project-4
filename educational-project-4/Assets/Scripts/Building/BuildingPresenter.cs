using Dialogs.BuildingDialog;
using UnityEngine;
using Utilities;

namespace Building
{
    public class BuildingPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildingModel _model;
        private readonly BuildingView _view;

        private BuildingDialogView _currentDialog;

        public BuildingPresenter(GameManager manager, BuildingModel model, BuildingView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.OnClick -= CreateDialog;
            _model.OnDialogClosed -= CloseDialog;
            _model.OnLevelUpdated -= LevelUpdated;
        }

        public void Activate()
        {
            _view.OnClick += CreateDialog;
            _model.OnDialogClosed += CloseDialog;
            _model.OnLevelUpdated += LevelUpdated;
        }

        private void LevelUpdated()
        {
            if (_model.Specification.Category.Equals("Главное"))
            {
                Debug.Log(_model.CurrentUpgradeLevel);
                // _manager.ExpansionModel.UpdateExpansionLevel(_model.CurrentUpgradeLevel);
            }
        }

        private void CreateDialog()
        {
            var activeModel = _manager.StatisticModel.Buildings.Find(item => item.HasActiveDialog);

            if (activeModel != null && activeModel == _model)
            {
                _model.CloseDialog();
                return;
            }

            activeModel?.CloseDialog();
            
            _model.HasActiveDialog = true;
            
            var neededDialogSpecification = _manager.Specifications.BuildingDialogs.Find(item => item.Id == _model.Specification.DialogId);
            var model = new BuildingDialogModel(_model.Specification);
            var view = _view.InstantiateDialogView(neededDialogSpecification?.Specification, _model.CurrentUpgradeLevel, neededDialogSpecification?.PreviewImage);
            var presenter = new BuildingDialogPresenter(_manager, model, view);
            
            presenter.Activate();

            _currentDialog = view;
        }
        
        private void CloseDialog()
        {
            _view.DestroyDialog(_currentDialog);
            _currentDialog = null;
        }
    }
}