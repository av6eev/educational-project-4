using System;

namespace Grid.GridExpansion
{
    public class GridExpansionModel
    {
        public event Action OnLevelUpdate;
        
        public int CurrentExpansionBuildingLevel;

        public GridExpansionModel()
        {
            CurrentExpansionBuildingLevel = (int)ExpansionBuildingLevels.Default;
        }

        public void UpdateExpansionLevel(int buildingLevel)
        {
            CurrentExpansionBuildingLevel = buildingLevel;
            OnLevelUpdate?.Invoke();
        }
    }
}