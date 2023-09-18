using BuildingsStatistic;
using Core.Game;
using Dialogs.Base;
using Save;
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
            var gameSpecifications = new GameSpecifications(StartView.SpecificationsCollection);
            
            _manager = new GameManager(
                StartView, 
                gameSpecifications, 
                new SceneLoaderPresenter(), 
                _systems, 
                new DialogsModel(gameSpecifications), 
                new BuildingsStatisticModel(gameSpecifications.BuildsCategory),
                new SaveModel());
            
            _presenters.Add(new CoreStartGamePresenter(_manager, StartView));
            
            _presenters.Activate();
        }

        public void Update()
        {
            _systems.Update(Time.deltaTime);
        }

        private void OnApplicationQuit()
        {
            _manager.SaveModel.Save();
            
            _presenters.Deactivate();
            _presenters.Clear();
        }
    }
}