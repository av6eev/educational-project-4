using BuildingsStatistic;
using Dialogs.Base;
using Requirements.Capitol.Place;
using Save;
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
            _presenters.Add(new DialogsPresenter(_manager, _manager.DialogsModel, _view.DialogsView));
            _presenters.Add(new SavePresenter(_manager, _manager.SaveModel));

            foreach (var requirement in _manager.Specifications.Requirements)
            {
                switch (requirement.Value)
                {
                    case CapitolPlaceRequirement model:
                        var presenter = new CapitolPlaceRequirementPresenter(_manager, model);
                        _presenters.Add(presenter);
                        break;
                }
            }
            
            _presenters.Activate();
            
            _manager.SceneLoader.SwitchScene("CosmicScene", _manager);
        }
    }
}