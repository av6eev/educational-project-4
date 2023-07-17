using Earth.Scene;
using Utilities;

namespace Earth
{
    public class EarthLocationManager : GameManager
    {
        public readonly GameManager GameManager;
        
        public EarthSceneView EarthSceneView;
        
        public EarthLocationManager(GameManager manager) : base(manager.CoreStartView, manager.Descriptions, manager.SceneLoader, manager.SystemEngine, manager.BuildDialogModel, manager.StatisticModel)
        {
            GameManager = manager;
        }
    }
}