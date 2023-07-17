using Cosmic.Scene;
using Utilities;

namespace Cosmic
{
    public class CosmicLocationManager : GameManager
    {
        public readonly GameManager GameManager;
        
        public CosmicSceneView CosmicSceneView;
        
        public CosmicLocationManager(GameManager manager) : base(manager.CoreStartView, manager.Descriptions, manager.SceneLoader, manager.SystemEngine, manager.BuildDialogModel, manager.StatisticModel)
        {
            GameManager = manager;
        }
    }
}