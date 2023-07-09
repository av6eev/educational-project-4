﻿using UnityEngine;
using Utilities;

namespace Grid.GridExpansion
{
    public class GridExpansionPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly GridExpansionModel _model;

        public GridExpansionPresenter(GameManager manager, GridExpansionModel model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            _model.OnLevelUpdate -= ExpandTerritory;
        }

        public void Activate()
        {
            _model.OnLevelUpdate += ExpandTerritory;
        }

        private void ExpandTerritory()
        {
            var gridView = _manager.GameView.GridView;
            float territoryUpgrade;
            int scale;
            
            switch (_model.CurrentExpansionBuildingLevel)
            {
                case (int)ExpansionBuildingLevels.FifthLevel:
                    territoryUpgrade = 1f / (float)ExpansionLevelsScaleUpgrade.FifthLevel;
                    scale = (int)ExpansionLevelsScaleUpgrade.FifthLevel;
                    break;
                case (int)ExpansionBuildingLevels.TenthLevel:
                    territoryUpgrade = 1f / (float)ExpansionLevelsScaleUpgrade.TenthLevel;
                    scale = (int)ExpansionLevelsScaleUpgrade.TenthLevel;
                    break;
                case (int)ExpansionBuildingLevels.FifteenthLevel:
                    territoryUpgrade = 1f / (float)ExpansionLevelsScaleUpgrade.FifteenthLevel;
                    scale = (int)ExpansionLevelsScaleUpgrade.FifteenthLevel;
                    break;
                case (int)ExpansionBuildingLevels.TwentiethLevel:
                    territoryUpgrade = 1f / (float)ExpansionLevelsScaleUpgrade.TwentiethLevel;
                    scale = (int)ExpansionLevelsScaleUpgrade.TwentiethLevel;
                    break;
                default:
                    return;
            }

            gridView.Grid.cellSize = new Vector3(territoryUpgrade, territoryUpgrade, territoryUpgrade);
            gridView.GridRoot.transform.localScale = new Vector3(scale, scale, scale);
            _model.UpdateGridSize(scale);
        }
    }
}