using Earth.Grid;
using Earth.Grid.Expansion;
using Utilities;

namespace Earth
{
    public class EarthPresenter : IPresenter
    {
        private readonly EarthLocationManager _manager;
        private readonly EarthModel _model;
        private readonly EarthView _view;
        
        private readonly PresentersEngine _presenters = new();

        public EarthPresenter(EarthLocationManager manager, EarthModel model, EarthView view)
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
            _manager.EarthGridModel = new EarthGridModel();
            _manager.ExpansionModel = new EarthGridExpansionModel();
        
            _manager.GameManager.SystemEngine.Add(SystemTypes.EarthGridPlacement, new EarthGridPlacementSystem(_manager));
            
            _presenters.Add(new EarthGridPresenter(_manager, _manager.EarthGridModel, _view.EarthGridView));
            _presenters.Add(new EarthGridExpansionPresenter(_manager, _manager.ExpansionModel));
        }
    }
}
