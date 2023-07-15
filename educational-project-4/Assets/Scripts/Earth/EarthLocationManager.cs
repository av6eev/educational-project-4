using BuildDialog;
using Earth.Scene;
using Grid;
using Grid.GridExpansion;
using GridBuildingsStatistic;
using Utilities;

namespace Earth
{
    public class EarthLocationManager : GameManager
    {
        public readonly GameManager GameManager;
        
        public EarthSceneView EarthSceneView;
        
        public GridModel GridModel;
        public BuildDialogModel BuildDialogModel;
        public GridBuildingsStatisticModel StatisticModel;
        public GridExpansionModel ExpansionModel;
        
        public EarthLocationManager(GameManager manager) : base(manager.View, manager.Descriptions, manager.SceneLoader, manager.SystemEngine)
        {
            GameManager = manager;
        }
    }
}