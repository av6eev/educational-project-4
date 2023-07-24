using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;

namespace Utilities
{
    public abstract class PlacementHandlerModel
    {
        public abstract BuildingDescription LastSelectedBuilding { get; set; }
        
        public abstract void PlaceBuilding(Vector3 gridPosition);

        public virtual void SelectBuilding(BuildingDescription description)
        {
            LastSelectedBuilding = description;
        }
    }
}