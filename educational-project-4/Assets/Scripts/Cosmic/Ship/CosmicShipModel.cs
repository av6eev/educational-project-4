using System;
using System.Collections.Generic;
using Cosmic.Ship.Floor;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;
using Utilities;

namespace Cosmic.Ship
{
    public class CosmicShipModel : PlacementHandlerModel
    {
        public event Action<Vector3> OnBuildingPlaced;
        public event Action OnBuildingSelected;
        public event Action<int> OnFloorChanged;
        
        public readonly List<CosmicShipFloorModel> Floors = new();
        public override BuildingDescription LastSelectedBuilding { get; set; }
        
        public override void PlaceBuilding(Vector3 gridPosition)
        {
            OnBuildingPlaced?.Invoke(gridPosition);
        }

        public override void SelectBuilding(BuildingDescription description)
        {
            base.SelectBuilding(description);
            OnBuildingSelected?.Invoke();
        }

        public void ChangeFloor(int nextFloorDir)
        {
            OnFloorChanged?.Invoke(nextFloorDir);
        }
    }
}