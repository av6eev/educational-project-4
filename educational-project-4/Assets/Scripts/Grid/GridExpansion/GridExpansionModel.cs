using System;
using UnityEngine;

namespace Grid.GridExpansion
{
    public class GridExpansionModel
    {
        public event Action OnLevelUpdate;
        
        public int CurrentExpansionBuildingLevel;
        public Vector2 CurrentGridSize;

        public GridExpansionModel()
        {
            CurrentExpansionBuildingLevel = (int)ExpansionBuildingLevels.Default;
            CurrentGridSize = new Vector2((int)ExpansionLevelsScaleUpgrade.Default * 5, (int)ExpansionLevelsScaleUpgrade.Default * 5);
        }

        public void UpdateExpansionLevel(int buildingLevel)
        {
            CurrentExpansionBuildingLevel = buildingLevel;
            OnLevelUpdate?.Invoke();
        }
    }
}