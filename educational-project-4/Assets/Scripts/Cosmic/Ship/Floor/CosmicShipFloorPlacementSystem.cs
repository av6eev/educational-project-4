using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Utilities.Helpers;

namespace Cosmic.Ship.Floor
{
    public class CosmicShipFloorPlacementSystem : BaseGridPlacementSystem, ISystem
    {
        private readonly CosmicLocationManager _manager;
        private readonly CosmicShipModel _shipModel;

        private CosmicShipFloorModel _activeFloor;
        
        public CosmicShipFloorPlacementSystem(CosmicLocationManager manager)
        {
            _manager = manager;
            _shipModel = _manager.GameManager.ShipModel;
        }
        
        public void Update(float deltaTime)
        {
            if (_shipModel.LastSelectedBuilding == null) return;
            
            _activeFloor = _shipModel.Floors.Find(floor => floor.IsActive);
            var mouseWorldPosition = GetMouseWorldPosition(_manager.CosmicSceneView.MainCamera);
            var cell = GetGridPosition(_activeFloor.Cells, mouseWorldPosition);
            
            var isValid = IsPlacementValid(cell, _shipModel.LastSelectedBuilding.Size);
            
            BuildingPreviewHelper.UpdatePreviewPosition(_manager.CosmicSceneView.CosmicShipView, cell, isValid);
            
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && _shipModel.LastSelectedBuilding != null && isValid)
            {
                _shipModel.PlaceBuilding(cell);
            }
        }

        protected override bool IsPlacementValid(Vector3Int cell, Vector2Int buildingSize)
        {
            foreach (var position in CalculatePosition(cell, buildingSize))
            {
                _activeFloor.Cells.TryGetValue(position, out var result);
                if (result == null) return false;
                if (_activeFloor.RegisteredBuildings.ContainsKey(position)) return false;
            }

            return true;
        }
    }
}