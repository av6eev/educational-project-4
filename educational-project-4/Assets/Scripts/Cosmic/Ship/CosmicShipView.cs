using Building;
using Core.Grid;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Cosmic.Ship
{
    public class CosmicShipView : BaseGridView
    {
        public CosmicShipFloorCellView FloorCellPrefab;

        private void Start()
        {
            CellSelectedIndicatorRenderer = CellSelectedIndicator.GetComponentInChildren<SpriteRenderer>();
            PreviewMaterialInstance = new Material(PreviewMaterial);
        }
        
        public CosmicShipFloorCellView InstantiateCell(Vector2 position)
        {
            var go = Instantiate(FloorCellPrefab, new Vector3(position.x + .5f, 0, position.y - .5f), Quaternion.identity);
            
            go.transform.SetParent(GridRoot.transform);
            
            return go;
        }

        public BuildingView InstantiateBuilding(BuildingView prefab, Vector2 gridPosition)
        {
            var go = Instantiate(prefab, new Vector3(gridPosition.x, .05f, gridPosition.y), Quaternion.identity);
            
            go.transform.SetParent(GridRoot.transform);

            return go;
        }
    }
}