using System.Collections.Generic;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Utilities
{
    public abstract class BaseGridPlacementSystem
    {
        protected virtual Vector3 GetMouseWorldPosition(Camera mainCamera, LayerMask layerMask)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.nearClipPlane;
            
            return Physics.Raycast(mainCamera.ScreenPointToRay(mousePosition), out var hit, 3000, layerMask) ? hit.point : Vector3.zero;
        }

        protected virtual CosmicShipFloorCellModel GetGridPosition(Dictionary<Vector2, CosmicShipFloorCellModel> cells, Vector3 mouseWorldPosition)
        {
            var position = new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.z);

            return cells.TryGetValue(position, out var cell) ? cell : null;
        }

        protected abstract bool IsPlacementValid(CosmicShipFloorCellModel cell, Vector2Int buildingSize);
        
        protected virtual List<Vector2> CalculatePosition(Vector2Int gridPosition, Vector2Int size)
        {
            var returnValues = new List<Vector2>();

            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    returnValues.Add(gridPosition + new Vector2(x,y));
                }
            }

            return returnValues;
        }
    }
}