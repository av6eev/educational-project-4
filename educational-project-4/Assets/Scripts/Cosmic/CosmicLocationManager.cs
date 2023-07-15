using Cosmic.Scene;
using Cosmic.Ship;
using Utilities;

namespace Cosmic
{
    public class CosmicLocationManager : GameManager
    {
        public readonly GameManager GameManager;
        
        public CosmicSceneView CosmicSceneView;
        
        public CosmicShipModel ShipModel;
       
        public CosmicLocationManager(GameManager manager) : base(manager.View, manager.Descriptions, manager.SceneLoader, manager.SystemEngine)
        {
            GameManager = manager;
        }
    }
}