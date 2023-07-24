using System;
using System.Collections.Generic;
using Building;
using Cosmic.Ship.Floor;
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
        
        private readonly PresentersEngine _floorsPresenters = new();
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
            _view.NextFloorBtn.onClick.RemoveListener(OnSelectNextFloor);
            _view.PreviousFloorBtn.onClick.RemoveListener(OnSelectPreviousFloor);
            
            _model.OnBuildingPlaced -= BuildingPlaced;
            _model.OnBuildingSelected -= BuildingSelected;

            _floorsPresenters.Deactivate();
            _floorsPresenters.Clear();
        }

        public void Activate()
        {
            CreateFloorData();
            
            _view.NextFloorBtn.onClick.AddListener(OnSelectNextFloor);
            _view.PreviousFloorBtn.onClick.AddListener(OnSelectPreviousFloor);
            
            _model.OnBuildingSelected += BuildingSelected;
            _model.OnBuildingPlaced += BuildingPlaced;
        }

        private void OnSelectNextFloor()
        {
            LoadFloors(1);    
        }
        
        private void OnSelectPreviousFloor()
        {
            LoadFloors(-1);    
        }
        
        private void BuildingPlaced(Vector3 gridPosition)
        {
            if (_gameManager.StatisticModel.BuildingLimits[_model.LastSelectedBuilding.Id] == 0)
            {
                _gameManager.StatisticModel.UpdateLimit(_model.LastSelectedBuilding.Id);
                StopShowPreview();
                return;
            }
            
            CreateNewBuilding(gridPosition, _model.LastSelectedBuilding);
            
            _gameManager.CoreStartView.BuildDialogView.BuildingRoot.SetActive(true);
            _gameManager.CoreStartView.BuildDialogView.CategoriesRoot.SetActive(true);
            
            ChangePlacementMode(false);
        }
        
        private void CreateNewBuilding(Vector3 gridPosition, BuildingDescription description)
        {
            var takenPosition = new List<Vector3>();
            
            for (var x = 0; x < description.Size.x; x++)
            {
                for (var z = 0; z < description.Size.y; z++)
                {
                    takenPosition.Add(gridPosition + new Vector3(x, gridPosition.y, z));
                }
            }
            
            var model = new BuildingModel(description.Id, description, takenPosition);
            var activeFloor = _model.Floors.Find(floor => floor.IsActive);
            
            foreach (var position in takenPosition)
            {
                if (activeFloor.RegisteredBuildings.ContainsKey(position))
                {
                    throw new Exception($"Cell {position} already contains in dictionary");
                }

                activeFloor.RegisteredBuildings[position] = model;
            }
            
            var buildingView = _view.InstantiateBuilding(description.Prefab, gridPosition, activeFloor.Index);
            new BuildingPresenter(_gameManager, model, buildingView).Activate();
            
            _gameManager.StatisticModel.Buildings.Add(model);
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
        
        private void CreateFloorData()
        {
            foreach (var level in _manager.CosmicSceneView.CosmicView.LevelsPositions)
            {
                var model = new CosmicShipFloorModel(level.Key);
                
                if (level.Key == 0)
                {
                    model.IsActive = true;
                }
                
                var view = _view.LevelFloorRoots[level.Key];
                var presenter = new CosmicShipFloorPresenter(_manager, model, view);    
                
                presenter.Activate();
                
                _floorsPresenters.Add(presenter);
                _model.Floors.Add(model);
            }

            LoadFloors();
        }

        private void LoadFloors(int nextFloorDir = 0)
        {
            var floorViews = _view.LevelFloorRoots;
            var floorModels = _model.Floors;
            var currentFloor = floorModels.Find(item => item.IsActive);
            CosmicShipFloorModel nextFloor;

            if (currentFloor.Index == 0 && nextFloorDir == -1) return;
            
            if (nextFloorDir != 0)
            {
                nextFloor = floorModels.Find(item => item.Index == currentFloor.Index + nextFloorDir);
            }
            else
            {
                nextFloor = floorModels[currentFloor.Index + 1];
                currentFloor.Load();
                currentFloor.IsActive = true;
                
                nextFloor.Load();
                
                floorViews[currentFloor.Index].gameObject.SetActive(true);
                floorViews[nextFloor.Index].gameObject.SetActive(false);

                foreach (var floor in floorModels.FindAll(item => item != currentFloor && item != nextFloor))
                {
                    floor.Unload();
                }
                
                return;
            }

            if (nextFloor == null) return;

            var nextByNextFloor = nextFloorDir switch
            {
                1 => floorModels.Find(item => item.Index == nextFloor!.Index + 1),
                -1 => floorModels.Find(item => item.Index == nextFloor!.Index - 1),
                _ => null
            };

            foreach (var floor in floorModels)
            {
                if (floor == currentFloor)
                {
                    floorViews[floor.Index].gameObject.SetActive(false);
                    currentFloor.IsActive = false;
                    continue;
                }
                
                if (floor == nextFloor)
                {
                    floorViews[floor.Index].gameObject.SetActive(true);
                    nextFloor.IsActive = true;
                    continue;
                }
                
                if (nextByNextFloor != null && floor == nextByNextFloor && floorViews[nextByNextFloor.Index] != null && nextFloor.Index != 0)
                {
                    _model.Floors[floor.Index].Load();
                    floorViews[floor.Index].gameObject.SetActive(false);    
                    continue;
                }
                
                _model.Floors[floor.Index].Unload();          
            }            
        }
    }
}