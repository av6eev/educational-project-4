using BuildDialog;
using Earth.Scene;
using Grid;
using Grid.GridExpansion;
using GridBuildingsStatistic;
using UnityEngine;
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
            _manager.GridModel = new GridModel();
            _manager.BuildDialogModel = new BuildDialogModel(_manager.Descriptions.Builds);
            _manager.StatisticModel = new GridBuildingsStatisticModel(_manager.Descriptions.BuildsCategory);
            _manager.ExpansionModel = new GridExpansionModel();
        
            _manager.GameManager.SystemEngine.Add(SystemTypes.EarthGridPlacement, new EarthGridPlacementSystem(_manager));
        
            _presenters.Add(new GridBuildingsStatisticPresenter(_manager, _manager.StatisticModel));
            _presenters.Add(new BuildDialogPresenter(_manager, _manager.BuildDialogModel, _view.BuildDialogView));
            _presenters.Add(new GridPresenter(_manager, _manager.GridModel, _view.GridView));
            _presenters.Add(new GridExpansionPresenter(_manager, _manager.ExpansionModel));
        }
    }
}
