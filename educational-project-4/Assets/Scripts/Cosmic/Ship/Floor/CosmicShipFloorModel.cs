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
        
        private int _index;
        public int Index
        {
            get => _index;
            private set
            {
                if (value < 0) return;
                _index = value;
            }
        }

        public bool IsActive { get; set; }
        public int AccessLevel { get; }
        public Dictionary<Vector3, CosmicShipFloorCellModel> Cells { get; private set; }= new();
        public Dictionary<Vector3, BuildingModel> RegisteredBuildings { get; } = new();

        public CosmicShipFloorModel(int index, int accessLevel)
        {
            Index = index;
            AccessLevel = accessLevel;
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