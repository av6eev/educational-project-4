using Specifications.Builds.Buildings;
using UnityEngine;

namespace Utilities
{
    public abstract class PlacementHandlerModel
    {
        public abstract BuildingSpecification LastSelectedBuilding { get; set; }
        
        public abstract void PlaceBuilding(Vector3 gridPosition);

        public virtual void SelectBuilding(BuildingSpecification specification)
        {
            LastSelectedBuilding = specification;
        }
    }
}