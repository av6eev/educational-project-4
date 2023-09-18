using Cosmic.Scene;
using Utilities;

namespace Cosmic
{
    public class CosmicLocationManager : GameManager
    {
        public readonly GameManager GameManager;
        
        public CosmicSceneView CosmicSceneView;
        
        public CosmicLocationManager(GameManager manager) : base(manager.CoreStartView, manager.Specifications, manager.SceneLoader, manager.SystemEngine, manager.DialogsModel, manager.StatisticModel, manager.SaveModel)
        {
            GameManager = manager;
        }
    }
}