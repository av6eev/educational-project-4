using System.Reflection;
using Cosmic.Ship;
using Cosmic.Ship.FloorCell;
using Utilities;

namespace Cosmic
{
    public class CosmicPresenter : IPresenter
    {
        private readonly CosmicLocationManager _manager;
        private readonly CosmicModel _model;
        private readonly CosmicView _view;

        private readonly PresentersEngine _presenters = new();
        
        public CosmicPresenter(CosmicLocationManager manager, CosmicModel model, CosmicView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _presenters.Deactivate();    
            _presenters.Clear();
        }

        public void Activate()
        {
            CreateNecessaryData();
            
            _presenters.Activate();
        }

        private void CreateNecessaryData()
        {
            var shipModel = new CosmicShipModel();
            
            _manager.ShipModel = shipModel;
            _manager.GameManager.SystemEngine.Add(SystemTypes.EarthGridPlacement, new CosmicShipFloorPlacementSystem(_manager));

            _presenters.Add(new CosmicShipPresenter(_manager, shipModel, _manager.CosmicSceneView.CosmicShipView));
        }
    }
}