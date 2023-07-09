using System;
using UnityEngine;

namespace Grid.GridExpansion
{
    public class GridExpansionModel
    {
        public event Action OnLevelUpdate;
        
        public int CurrentExpansionBuildingLevel;
        public Vector2 CurrentGridSize { get; private set; }

        public GridExpansionModel()
        {
            CurrentExpansionBuildingLevel = (int)ExpansionBuildingLevels.Default;
            UpdateGridSize((int)ExpansionLevelsScaleUpgrade.Default);
        }

        public void UpdateExpansionLevel(int buildingLevel)
        {
            CurrentExpansionBuildingLevel = buildingLevel;
            OnLevelUpdate?.Invoke();
        }

        public void UpdateGridSize(int scale)
        {
            CurrentGridSize = new Vector2(scale * 5, scale * 5);
        }
    }
}