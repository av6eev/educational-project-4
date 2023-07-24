using System;
using System.Collections.Generic;
using Building;
using Cosmic.Ship.FloorCell;
using UnityEngine;

namespace Cosmic.Ship.Floor
{
    public class CosmicShipFloorModel
    {
        public event Action OnLoad, OnUnload; 
        public int Index { get; private set; }
        public bool IsActive { get; set; }
        public readonly Dictionary<Vector3, CosmicShipFloorCellModel> Cells = new();
        public Dictionary<Vector3, BuildingModel> RegisteredBuildings { get; } = new();

        public CosmicShipFloorModel(int index)
        {
            Index = index;
        }

        public void Load()
        {
            OnLoad?.Invoke();
        }

        public void Unload()
        {
            OnUnload?.Invoke();
        }
    }
}