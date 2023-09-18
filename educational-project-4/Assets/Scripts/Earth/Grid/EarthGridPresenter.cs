using System;
using Building;
using Specifications.Builds.Buildings;
using UnityEngine;
using Utilities;

namespace Earth.Grid
{
    public class EarthGridPresenter : IPresenter
    {
        private readonly EarthLocationManager _manager;
        private readonly EarthGridModel _model;
        private readonly EarthGridView _view;

        public EarthGridPresenter(EarthLocationManager manager, EarthGridModel model, EarthGridView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _model.OnBuildingSelected -= BuildingSelected;
            _model.OnPlacementModeChanged -= ChangePlacementMode;
            _model.OnBuildingPlaced -= PlaceBuilding;
        }

        public void Activate()
        {
            ChangePlacementMode(false);
            
            _model.OnBuildingSelected += BuildingSelected;
            _model.OnPlacementModeChanged += ChangePlacementMode;
            _model.OnBuildingPlaced += PlaceBuilding;
        }
        
        private void BuildingSelected()
        {
            if (_view.PreviewObject != null)
            {
                StopShowPreview();
            }
            
            _view.CreatePreview(_model.LastSelectedBuilding.Prefab);
            
            ChangePlacementMode(true);
        }
        
        private void StopShowPreview()
        {
            _view.DestroyPreview();
            _model.LastSelectedBuilding = null;
        }

        private void StartShowPreview(Vector2Int size)
        {
            SettingPreview(_view.PreviewObject);
            SettingCursor(size);
            
            _view.CellSelectedIndicator.SetActive(true);
        }

        private void PlaceBuilding()
        {
            var gridPosition = _view.Grid.WorldToCell(_manager.EarthGridModel.LastPosition);

            if (_manager.StatisticModel.BuildingLimits[_model.LastSelectedBuilding.Id] == 0)
            {
                // _manager.StatisticModel.UpdateLimit(_model.LastSelectedBuilding.Id);
                StopShowPreview();
                return;
            }
            
            AddNewBuilding(gridPosition, _model.LastSelectedBuilding);
            
            _manager.EarthSceneView.EarthView.BuildDialogView.BuildingRoot.SetActive(true);
            _manager.EarthSceneView.EarthView.BuildDialogView.CategoriesRoot.SetActive(true);
            
            ChangePlacementMode(false);
        }
        
        private void AddNewBuilding(Vector3Int gridPosition, BuildingSpecification specification)
        {
            // var takenPosition = _model.CalculatePosition(gridPosition, specification.Size);
            // var model = new BuildingModel(specification.Id, specification, takenPosition);
            // var instantiateResult = _view.CreateBuilding(specification.Prefab, gridPosition);
            // new BuildingPresenter(_manager, model, instantiateResult.Item2).Activate();
            //
            // _manager.StatisticModel.Buildings.Add(model);
            //
            // foreach (var position in takenPosition)
            // {
            //    if (_model.RegisteredBuildings.ContainsKey(position))
            //    {
            //         throw new Exception($"Cell {position} already contains in dictionary");
            //    }
            //
            //    _model.RegisteredBuildings[position] = model;
            // }
            //
            // _manager.StatisticModel.UpdateLimit(model.Id);
        }
     
        private void ChangePlacementMode(bool state)
        {
            switch (state)
            {
                case true:
                    StartShowPreview(_model.LastSelectedBuilding.Size);
                    _manager.EarthSceneView.EarthView.BuildDialogView.BuildingRoot.SetActive(false);
                    _manager.EarthSceneView.EarthView.BuildDialogView.CategoriesRoot.SetActive(false);
                    break;
                case false:
                    if (_model.LastSelectedBuilding != null)
                    {
                        StopShowPreview();
                    }

                    _view.CellSelectedIndicator.SetActive(false);
                    _model.LastDetectedPosition = Vector3Int.zero;
                    break;
            }
            
            _view.GridRoot.SetActive(state);
        }
        
        private void SettingCursor(Vector2Int size)
        {
            if (size is { x: <= 0, y: <= 0 }) return;
            
            _view.CellSelectedIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            _view.CellSelectedIndicatorRenderer.material.mainTextureScale = size;
        }

        private void SettingPreview(Component previewObject)
        {
            var renderers = previewObject.GetComponentsInChildren<Renderer>();
            
            foreach (var renderer in renderers)
            {
                var materials = renderer.materials;
                
                for (var i = 0; i < materials.Length; i++)
                {
                    materials[i] = _view.PreviewMaterialInstance;
                }

                renderer.materials = materials;
            }
        }
    }
}