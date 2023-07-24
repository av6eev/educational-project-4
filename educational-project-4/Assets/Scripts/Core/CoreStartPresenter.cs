using BuildingsStatistic;
using Core.Game;
using Dialogs.Base;
using Dialogs.BuildDialog;
using UnityEngine;
using Utilities;

namespace Core
{
    public class CoreStartPresenter : MonoBehaviour
    {
        public CoreStartView StartView;
        
        private GameManager _manager;
        private readonly PresentersEngine _presenters = new();
        private readonly SystemsEngine _systems = new();

        public void Start()
        {
            var gameDescriptions = new GameDescriptions(StartView.DescriptionsCollection);
            _manager = new GameManager(StartView, gameDescriptions, new SceneLoaderPresenter(), _systems, new DialogsModel(gameDescriptions), new BuildingsStatisticModel(gameDescriptions.BuildsCategory));
            
            _presenters.Add(new CoreStartGamePresenter(_manager, StartView));
            
            _presenters.Activate();
        }

        public void Update()
        {
            _systems.Update(Time.deltaTime);
        }
    }
}