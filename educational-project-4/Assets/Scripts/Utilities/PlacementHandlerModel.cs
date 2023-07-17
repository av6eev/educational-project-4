using System.Collections.Generic;
using Building;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;

namespace Utilities
{
    public abstract class PlacementHandlerModel
    {
        public abstract BuildingDescription LastSelectedBuilding { get; set; }
        public abstract Dictionary<Vector2, BuildingModel> RegisteredBuildings { get; protected set; }

        public abstract void PlaceBuilding(Vector2 gridPosition);

        public virtual void SelectBuilding(BuildingDescription description)
        {
            LastSelectedBuilding = description;
        }
    }
}