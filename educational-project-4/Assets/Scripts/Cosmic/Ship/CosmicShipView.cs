using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Cosmic.Ship
{
    public class CosmicShipView : MonoBehaviour
    {
        public Transform FloorRoot;
        public CosmicShipFloorCellView FloorCellPrefab;

        public CosmicShipFloorCellView InstantiateCell(Vector3 position)
        {
            var go = Instantiate(FloorCellPrefab, position, Quaternion.identity);
            
            go.transform.SetParent(FloorRoot);
            
            return go;
        }
    }
}