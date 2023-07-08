using System;
using System.Collections.Generic;
using Building;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;

namespace Grid
{
    public class GridModel
    {
        public event Action<bool> OnPlacementModeChanged;
        public event Action OnBuildingPlaced, OnBuildingSelected;

        public Vector3 LastPosition;
        public Vector3Int LastDetectedPosition = Vector3Int.zero;

        public readonly Dictionary<Vector3Int, BuildingModel> RegisteredBuildings = new();
        public BuildingDescription LastSelectedBuilding;
        
        public void ChangePlacementMode(bool state)
        {
            OnPlacementModeChanged?.Invoke(state);
        }

        public void PlaceBuilding()
        {
            OnBuildingPlaced?.Invoke();
        }

        public void SelectBuilding(BuildingDescription description)
        {
            LastSelectedBuilding = description;
            OnBuildingSelected?.Invoke();
        }
        
        public bool IsPlacementValid(Vector3Int gridPosition, Vector2 gridSize)
        {
            foreach (var position in CalculatePosition(gridPosition, LastSelectedBuilding.Size))
            {
                if (position.x >= gridSize.x || position.z >= gridSize.y) return false;
                if (RegisteredBuildings.ContainsKey(position)) return false;
            }

            return true;
        }
        
        public List<Vector3Int> CalculatePosition(Vector3Int gridPosition, Vector2Int size)
        {
            var returnValues = new List<Vector3Int>();

            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    returnValues.Add(gridPosition + new Vector3Int(x, 0, y));
                }
            }

            return returnValues;
        }
    }
}