using BuildDialog;
using BuildingsStatistic;
using UnityEngine;
using Utilities;

namespace Core.Game
{
    public class CoreStartGamePresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly CoreStartView _view;

        private AsyncOperation _sceneLoader;
        
        private readonly PresentersEngine _presenters = new();
        
        public CoreStartGamePresenter(GameManager manager, CoreStartView view)
        {
            _manager = manager;
            _view = view;
        }
        
        public void Deactivate()
        {
            _presenters.Deactivate();
            _presenters.Clear();
        }

        public void Activate()
        {
            _presenters.Add(new BuildingsStatisticPresenter(_manager, _manager.StatisticModel));
            _presenters.Add(new BuildDialogPresenter(_manager, _manager.BuildDialogModel, _view.BuildDialogView));
            
            _presenters.Activate();
            
            _manager.SceneLoader.SwitchScene("CosmicScene", _manager);
        }
    }
}