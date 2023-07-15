using Core;

namespace Utilities
{
    public class GameManager
    {
        public CoreStartView View { get; }
        public SceneLoaderPresenter SceneLoader { get; }
        public SystemsEngine SystemEngine { get; }
        public GameDescriptions Descriptions { get; }

        public GameManager(CoreStartView view, GameDescriptions descriptions, SceneLoaderPresenter sceneLoader, SystemsEngine systemEngine)
        {
            Descriptions = descriptions;
            View = view;
            SceneLoader = sceneLoader;
            SystemEngine = systemEngine;
        }
    }
}