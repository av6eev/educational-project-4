using Cosmic;
using Cosmic.Scene;
using Earth;
using Earth.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.String;

namespace Utilities
{
    public class SceneLoaderPresenter
    {
        private AsyncOperation _sceneLoader;
        private IPresenter _presenter;
        private string _currentSceneTitle = Empty;
        private GameManager _gameManager;
        
        public void SwitchScene(string sceneTitle, GameManager gameManager)
        {
            _gameManager ??= gameManager;
            
            if (_currentSceneTitle != Empty)
            {
                _presenter.Deactivate();
                _presenter = null;
                
                _sceneLoader = SceneManager.UnloadSceneAsync($"{_currentSceneTitle}");

                _currentSceneTitle = sceneTitle;
                
                _sceneLoader.completed += OnCompleteUnloadScene;
                return;
            }
            
            _sceneLoader = SceneManager.LoadSceneAsync($"{sceneTitle}", LoadSceneMode.Additive);
            _sceneLoader.completed += OnCompleteLoadScene;
        }

        private void OnCompleteUnloadScene(AsyncOperation operation)
        {
            _sceneLoader.completed -= OnCompleteUnloadScene;
            
            _sceneLoader = SceneManager.LoadSceneAsync($"{_currentSceneTitle}", LoadSceneMode.Additive);
            _sceneLoader.completed += OnCompleteLoadScene;
        }

        private void OnCompleteLoadScene(AsyncOperation operation)
        {
            _sceneLoader.completed -= OnCompleteLoadScene;
            
            var gameSceneView = GameObject.Find("game_scene_view").GetComponent<GameSceneView>();

            switch (gameSceneView)
            {
                case CosmicSceneView view:
                    var cosmicLocationManager = new CosmicLocationManager(_gameManager);
                    var cosmicModel = new CosmicModel();
                    
                    cosmicLocationManager.CosmicSceneView = view;
                    
                    _presenter = new CosmicPresenter(cosmicLocationManager, cosmicModel, view.CosmicView);
                    break;
                case EarthSceneView view:
                    var earthLocationManager = new EarthLocationManager(_gameManager);
                    var earthModel = new EarthModel();
                    
                    earthLocationManager.EarthSceneView = view;
                    
                    _presenter = new EarthPresenter(earthLocationManager, earthModel, view.EarthView);
                    break;
            }
            
            _presenter.Activate();
        }
    }
}