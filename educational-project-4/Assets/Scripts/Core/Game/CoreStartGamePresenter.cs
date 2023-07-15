using UnityEngine;
using Utilities;

namespace Core.Game
{
    public class CoreStartGamePresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly CoreStartView _view;

        private AsyncOperation _sceneLoader;

        public CoreStartGamePresenter(GameManager manager, CoreStartView view)
        {
            _manager = manager;
            _view = view;
        }
        
        public void Deactivate()
        {
        }

        public void Activate()
        {
            _manager.SceneLoader.SwitchScene("CosmicScene", _manager);
        }
    }
}