using System;
using System.Linq;
using Requirements.Base;
using UnityEngine;
using Utilities;

namespace Requirements.Capitol.FifthLevel
{
    [Serializable]
    public class CapitolFifthLevelRequirement : ComplexRequirement
    {
        public override bool Completed { get; set; }
        public override SubRequirementType SubType { get; } = SubRequirementType.CapitolLevel;
        [field: SerializeField] public override string RewardName { get; protected set; }
        [field: SerializeField] public override string SideRewardName { get; protected set; }
        [field: SerializeField] public int RequireLevel  { get; protected set; }
        [field: NonSerialized] public int CurrentLevel { get; private set; }

        public override bool Check(GameManager manager)
        {
            var mainBuilding = manager.ShipModel.GetActiveFloor.RegisteredBuildings.Values.First(item => item.Specification.Category == "Главное");
            
            CurrentLevel = mainBuilding.CurrentUpgradeLevel;
            
            if (CurrentLevel != RequireLevel)
            {
                return false;
            }
            
            manager.Specifications.Rewards[RewardName].Give(manager);
            
            Completed = true;
            return true;
        }
    }
}