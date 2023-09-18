using System;
using System.Collections.Generic;
using Cosmic.Ship.Floor;
using Specifications.Builds.Buildings;
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
        public CosmicShipFloorModel GetActiveFloor => Floors.Find(floor => floor.IsActive);
        public override BuildingSpecification LastSelectedBuilding { get; set; }
        
        public override void PlaceBuilding(Vector3 gridPosition)
        {
            OnBuildingPlaced?.Invoke(gridPosition);
        }

        public override void SelectBuilding(BuildingSpecification specification)
        {
            base.SelectBuilding(specification);
            OnBuildingSelected?.Invoke();
        }

        public void ChangeFloor(int nextFloorDir)
        {
            OnFloorChanged?.Invoke(nextFloorDir);
        }
    }
}