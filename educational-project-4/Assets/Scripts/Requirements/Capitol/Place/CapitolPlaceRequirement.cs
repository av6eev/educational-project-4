using System;
using System.Linq;
using Requirements.Base;
using UnityEngine;
using Utilities;

namespace Requirements.Capitol.Place
{
    [Serializable]
    public class CapitolPlaceRequirement : SingleRequirement
    {
        public override bool Completed { get; set; }
        public override SubRequirementType SubType { get; } = SubRequirementType.Place;
        [field: SerializeField] public override string RewardName { get; protected set; }
        [field: SerializeField] public string CapitolCategory { get; protected set; }
        
        public override bool Check(GameManager manager)
        {
            var capitolDescription = manager.Specifications.Buildings.First(specification => specification.Category == CapitolCategory);
            var capitolBuilding = manager.StatisticModel.Buildings.Find(building => building.Id.Equals(capitolDescription.Id));

            if (capitolBuilding == null) return false;
            
            manager.Specifications.Rewards[RewardName].Give(manager);

            Completed = true;
            return true;
        }
    }
}