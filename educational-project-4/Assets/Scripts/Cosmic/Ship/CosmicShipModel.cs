using System;
using System.Collections.Generic;
using Building;
using Cosmic.Ship.FloorCell;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;
using Utilities;

namespace Cosmic.Ship
{
    public class CosmicShipModel : PlacementHandlerModel
    {
        public event Action<Vector2> OnBuildingPlaced;
        public event Action OnBuildingSelected;
        
        public readonly Dictionary<Vector2, CosmicShipFloorCellModel> FloorCells = new();

        public override BuildingDescription LastSelectedBuilding { get; set; }
        public override Dictionary<Vector2, BuildingModel> RegisteredBuildings { get; protected set; } = new();
        
        public override void PlaceBuilding(Vector2 gridPosition)
        {
            OnBuildingPlaced?.Invoke(gridPosition);
        }

        public override void SelectBuilding(BuildingDescription description)
        {
            base.SelectBuilding(description);
            OnBuildingSelected?.Invoke();
        }
    }
}