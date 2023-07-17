using System;
using System.Collections.Generic;
using Building;
using Cosmic.Ship.FloorCell;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;
using Utilities;
using Utilities.Helpers;

namespace Cosmic.Ship
{
    public class CosmicShipPresenter : IPresenter
    {
        private readonly CosmicLocationManager _manager;
        private readonly CosmicShipModel _model;
        private readonly CosmicShipView _view;
        
        private readonly PresentersEngine _cellPresenters = new();
        private readonly GameManager _gameManager;

        public CosmicShipPresenter(CosmicLocationManager manager, CosmicShipModel model, CosmicShipView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
            _gameManager = _manager.GameManager;
        }
        
        public void Deactivate()
        {
            _model.OnBuildingPlaced -= CreateBuilding;
            _model.OnBuildingSelected -= BuildingSelected;
            
            _cellPresenters.Deactivate();
            _cellPresenters.Clear();
        }

        public void Activate()
        {
            GenerateCells();

            _model.OnBuildingSelected += BuildingSelected;
            _model.OnBuildingPlaced += CreateBuilding;
        }

        private void CreateBuilding(Vector2 gridPosition)
        {
            if (_gameManager.StatisticModel.BuildingLimits[_model.LastSelectedBuilding.Id] == 0)
            {
                _gameManager.StatisticModel.UpdateLimit(_model.LastSelectedBuilding.Id);
                StopShowPreview();
                return;
            }
            
            AddNewBuilding(gridPosition, _model.LastSelectedBuilding);
            
            _gameManager.CoreStartView.BuildDialogView.BuildingRoot.SetActive(true);
            _gameManager.CoreStartView.BuildDialogView.CategoriesRoot.SetActive(true);
            
            ChangePlacementMode(false);
        }
        
        private void AddNewBuilding(Vector2 gridPosition, BuildingDescription description)
        {
            var takenPosition = new List<Vector2>();
            
            for (var x = 0; x < description.Size.x; x++)
            {
                for (var y = 0; y < description.Size.y; y++)
                {
                    takenPosition.Add(gridPosition + new Vector2(x,y));
                }
            }
            
            var model = new BuildingModel(description.Id, description, takenPosition);
            var buildingView = _view.InstantiateBuilding(description.Prefab, gridPosition);
            new BuildingPresenter(_gameManager, model, buildingView).Activate();
            
            _gameManager.StatisticModel.Buildings.Add(model);

            foreach (var position in takenPosition)
            {
                if (_model.RegisteredBuildings.ContainsKey(position))
                {
                    throw new Exception($"Cell {position} already contains in dictionary");
                }

                _model.RegisteredBuildings[position] = model;
            }
            
            _gameManager.StatisticModel.UpdateLimit(model.Id);
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
        
        private void ChangePlacementMode(bool state)
        {
            switch (state)
            {
                case true:
                    StartShowPreview(_model.LastSelectedBuilding.Size);
                    _gameManager.CoreStartView.BuildDialogView.BuildingRoot.SetActive(false);
                    _gameManager.CoreStartView.BuildDialogView.CategoriesRoot.SetActive(false);
                    break;
                case false:
                    if (_model.LastSelectedBuilding != null)
                    {
                        StopShowPreview();
                    }

                    _view.CellSelectedIndicator.SetActive(false);
                    break;
            }
        }
        
        private void StopShowPreview()
        {
            _view.DestroyPreview();
            _model.LastSelectedBuilding = null;
        }

        private void StartShowPreview(Vector2Int size)
        {
            BuildingPreviewHelper.SettingPreview(_view);
            BuildingPreviewHelper.SettingCursor(_view, size);
            
            _view.CellSelectedIndicator.SetActive(true);
        }
        
        private void GenerateCells()
        {
            var positions = _manager.CosmicSceneView.CosmicView.DrawingHelper.Positions;
            var cells = CosmicShipFloorCalculationsHelper.DefineCellsInsideGivenArea(positions);

            foreach (var cell in cells)
            {
                var model = new CosmicShipFloorCellModel(cell);
                var presenter = new CosmicShipFloorCellPresenter(_gameManager, model, _view.InstantiateCell(cell));

                _model.FloorCells.Add(cell, model);
                _cellPresenters.Add(presenter);
            }

            _cellPresenters.Activate();
        }
    }
}