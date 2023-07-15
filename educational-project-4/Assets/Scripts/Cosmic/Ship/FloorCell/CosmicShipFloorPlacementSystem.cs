using UnityEngine;
using Utilities;

namespace Cosmic.Ship.FloorCell
{
    public class CosmicShipFloorPlacementSystem : BaseGridPlacementSystem, ISystem
    {
        private readonly CosmicLocationManager _manager;

        public CosmicShipFloorPlacementSystem(CosmicLocationManager manager)
        {
            _manager = manager;
        }
        
        public void Update(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
            }
        }
    }
}