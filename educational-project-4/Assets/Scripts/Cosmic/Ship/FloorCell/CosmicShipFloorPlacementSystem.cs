using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Utilities.Helpers;

namespace Cosmic.Ship.FloorCell
{
    public class CosmicShipFloorPlacementSystem : BaseGridPlacementSystem, ISystem
    {
        private readonly CosmicLocationManager _manager;
        private readonly CosmicShipModel _shipModel;
        
        public CosmicShipFloorPlacementSystem(CosmicLocationManager manager)
        {
            _manager = manager;
            _shipModel = _manager.GameManager.ShipModel;
        }
        
        public void Update(float deltaTime)
        {
            if (_shipModel.LastSelectedBuilding == null) return;

            var mouseWorldPosition = GetMouseWorldPosition(_manager.CosmicSceneView.MainCamera, _manager.CosmicSceneView.CosmicShipView.PlacementMask);
            var cell = GetGridPosition(_shipModel.FloorCells, mouseWorldPosition);
            
            if (cell == null) return;
            
            var isValid = IsPlacementValid(cell, _shipModel.LastSelectedBuilding.Size);
            
            BuildingPreviewHelper.UpdatePreviewPosition(_manager.CosmicSceneView.CosmicShipView, cell.Position, isValid);
            
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && _shipModel.LastSelectedBuilding != null && isValid)
            {
                _shipModel.PlaceBuilding(cell.Position);
            }
        }

        protected override bool IsPlacementValid(CosmicShipFloorCellModel cell, Vector2Int buildingSize)
        {
            foreach (var position in CalculatePosition(cell.Position, buildingSize))
            {
                if (_shipModel.RegisteredBuildings.ContainsKey(position)) return false;
                if (!_manager.GameManager.ShipModel.FloorCells.ContainsKey(position)) return false;
            }

            return true;
        }
    }
}