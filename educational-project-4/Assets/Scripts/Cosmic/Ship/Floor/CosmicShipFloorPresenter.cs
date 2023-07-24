using Cosmic.Ship.FloorCell;
using Utilities;
using Utilities.Helpers;

namespace Cosmic.Ship.Floor
{
    public class CosmicShipFloorPresenter : IPresenter
    {
        private readonly CosmicLocationManager _manager;
        private readonly CosmicShipFloorModel _model;
        private readonly CosmicShipFloorView _view;
        
        private readonly PresentersEngine _cellPresenters = new();

        public CosmicShipFloorPresenter(CosmicLocationManager manager, CosmicShipFloorModel model, CosmicShipFloorView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _model.OnLoad -= GenerateCells;
            _model.OnUnload -= DestroyCells;
            
            _cellPresenters.Deactivate();
            _cellPresenters.Clear();
        }

        public void Activate()
        {
            _model.OnLoad += GenerateCells;
            _model.OnUnload += DestroyCells;
        }

        private void GenerateCells()
        {
            var cells = CosmicShipFloorCalculationsHelper.DefineCellsInsideGivenArea(_manager.CosmicSceneView.CosmicView.LevelsPositions[_model.Index], false);
            var cellPresenters = new PresentersEngine();

            foreach (var cell in cells)
            {
                var model = new CosmicShipFloorCellModel(cell);
                var view = _view.InstantiateCell(cell);
                var presenter = new CosmicShipFloorCellPresenter(_manager, model, view);
                    
                _model.Cells.Add(cell, model);
                cellPresenters.Add(presenter);
            }
                
            _cellPresenters.Add(cellPresenters);
        }
        
        private void DestroyCells()
        {
            _model.IsActive = false;
            _model.Cells.Clear();
            
            _view.DestroyFloorCells();
            _manager.CosmicSceneView.CosmicShipView.LevelFloorRoots[_model.Index].gameObject.SetActive(false);

            _cellPresenters.Deactivate();
            _cellPresenters.Clear();
        }
    }
}