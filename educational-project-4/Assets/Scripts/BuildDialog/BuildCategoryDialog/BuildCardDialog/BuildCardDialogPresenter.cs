using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildCardDialogModel _model;
        private readonly BuildCardDialogView _view;

        public BuildCardDialogPresenter(GameManager manager, BuildCardDialogModel model, BuildCardDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.OnMouseDown -= OnClick;
            _model.OnLimitUpdated -= RedrawLimit;
        }

        public void Activate()
        {
            _view.OnMouseDown += OnClick;
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
            var sceneIndex = SceneManager.GetSceneAt(1).buildIndex;
            switch (sceneIndex)
            { 
                case (int) SceneNames.CosmicScene:
                    _manager.ShipModel.SelectBuilding(_model.Description);
                    break;
                case (int) SceneNames.EarthScene:
                    _manager.EarthGridModel.SelectBuilding(_model.Description);
                    break;
            }
        }
    }
}