using UnityEngine;

namespace Cosmic.Ship.FloorCell
{
    public class CosmicShipFloorCellModel
    {
        public Vector3Int Position { get; private set; }

        public CosmicShipFloorCellModel(Vector3Int position)
        {
            Position = position;
        }
    }
}