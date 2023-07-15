using System;
using Earth;
using Utilities;

namespace BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogPresenter : IPresenter
    {
        private readonly EarthLocationManager _manager;
        private readonly BuildCardDialogModel _model;
        private readonly BuildCardDialogView _view;

        public BuildCardDialogPresenter(EarthLocationManager manager, BuildCardDialogModel model, BuildCardDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.Down -= OnClick;
            _model.OnLimitUpdated -= RedrawLimit;
        }

        public void Activate()
        {
            _view.Down += OnClick;
            _model.OnLimitUpdated += RedrawLimit;
        }

        private void RedrawLimit(string currentLimit)
        {
            _view.LimitTxt.text = currentLimit;

            if (Convert.ToInt32(currentLimit) == 0)
            {
                _view.enabled = false;
            }
        }

        private void OnClick()
        {
            _manager.GridModel.SelectBuilding(_model.Description);
        }
    }
}