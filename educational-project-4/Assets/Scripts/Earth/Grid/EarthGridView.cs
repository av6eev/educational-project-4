﻿using Building;
using Core.Grid;
using UnityEngine;

namespace Earth.Grid
{
    public class EarthGridView : BaseGridView
    {
        public UnityEngine.Grid Grid;
        
        public (Vector3, BuildingView) CreateBuilding(BuildingView prefab, Vector3Int gridPosition)
        {
            var building = Instantiate(prefab);
            var result = (Vector3: building.transform.position = Grid.CellToWorld(gridPosition), BuildingView: building);
            return result;
        }
    }
}