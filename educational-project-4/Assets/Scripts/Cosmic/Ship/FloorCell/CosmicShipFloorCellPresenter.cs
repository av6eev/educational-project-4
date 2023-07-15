using Utilities;

namespace Cosmic.Ship.FloorCell
{
    public class CosmicShipFloorCellPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly CosmicShipFloorCellModel _model;
        private readonly CosmicShipFloorCellView _view;

        public CosmicShipFloorCellPresenter(GameManager manager, CosmicShipFloorCellModel model, CosmicShipFloorCellView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
        }

        public void Activate()
        {
        }
    }
}