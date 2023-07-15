using Core.Game;
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
            _manager = new GameManager(StartView, new GameDescriptions(StartView.DescriptionsCollection), new SceneLoaderPresenter(), _systems);
            
            _presenters.Add(new CoreStartGamePresenter(_manager, StartView));
            
            _presenters.Activate();
        }

        public void Update()
        {
            _systems.Update(Time.deltaTime);
        }
    }
}