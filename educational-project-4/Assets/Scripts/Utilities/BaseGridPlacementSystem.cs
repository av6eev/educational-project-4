using System.Collections.Generic;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Utilities
{
    public abstract class BaseGridPlacementSystem
    {
        protected virtual Vector3 GetMouseWorldPosition(Camera mainCamera)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane;
            
            return Physics.Raycast(mainCamera.ScreenPointToRay(mousePosition), out var hit, 3000) ? hit.point : Vector3.zero;
        }

        protected virtual Vector3Int GetGridPosition(Dictionary<Vector3, CosmicShipFloorCellModel> cells, Vector3 mouseWorldPosition)
        {
            return new Vector3Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y, (int)mouseWorldPosition.z);
        }

        protected abstract bool IsPlacementValid(Vector3Int cell, Vector2Int buildingSize);
        
        protected virtual List<Vector3> CalculatePosition(Vector3Int gridPosition, Vector2Int size)
        {
            var returnValues = new List<Vector3>();

            for (var x = 0; x < size.x; x++)
            {
                for (var z = 0; z < size.y; z++)
                {
                    returnValues.Add(gridPosition + new Vector3(x, 0, z));
                }
            }

            return returnValues;
        }
    }
}