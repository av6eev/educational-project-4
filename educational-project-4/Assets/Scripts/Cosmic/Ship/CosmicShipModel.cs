using System.Collections.Generic;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Cosmic.Ship
{
    public class CosmicShipModel
    {
        public Dictionary<Vector3, CosmicShipFloorCellModel> FloorCells = new();
    }
}