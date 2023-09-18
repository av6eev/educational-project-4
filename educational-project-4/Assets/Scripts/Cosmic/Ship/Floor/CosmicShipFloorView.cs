using System.Collections.Generic;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Cosmic.Ship.Floor
{
    public class CosmicShipFloorView : MonoBehaviour
    {
        [field: SerializeField] public CosmicShipFloorCellView FloorCellPrefab { get; private set; }
        
        private readonly List<CosmicShipFloorCellView> _cells = new();
        
        public CosmicShipFloorCellView InstantiateCell(Vector3Int position)
        {
            var view = Instantiate(FloorCellPrefab, new Vector3(position.x + .5f, position.y, position.z - .5f), Quaternion.identity);
            view.transform.SetParent(transform);
            
            _cells.Add(view);
            
            return view;
        }
        
        public void DestroyFloorCells()
        {
            if (_cells.Count == 0) return;
            
            foreach (var cell in _cells)
            {
                Destroy(cell.gameObject);
            }
            
            _cells.Clear();
        }
    }
}