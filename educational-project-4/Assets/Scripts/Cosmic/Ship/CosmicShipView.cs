using System.Collections.Generic;
using Building;
using Core.Grid;
using Cosmic.Ship.Floor;
using UnityEngine;

namespace Cosmic.Ship
{
    public class CosmicShipView : BaseGridView
    {
        [field: SerializeField] public List<CosmicShipFloorView> LevelFloorRoots { get; private set; }

        public BuildingView InstantiateBuilding(BuildingView prefab, Vector3 gridPosition, int floorLevel)
        {
            var go = Instantiate(prefab, new Vector3(gridPosition.x, gridPosition.y + .05f, gridPosition.z), Quaternion.identity);
            
            go.transform.SetParent(LevelFloorRoots[floorLevel].transform);

            return go;
        }
    }
}