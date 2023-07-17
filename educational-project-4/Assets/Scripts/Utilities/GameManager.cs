using BuildDialog;
using BuildingsStatistic;
using Core;
using Cosmic.Ship;
using Earth.Grid;
using Earth.Grid.GridExpansion;

namespace Utilities
{
    public class GameManager
    {
        public CoreStartView CoreStartView { get; }
        public SceneLoaderPresenter SceneLoader { get; }
        public SystemsEngine SystemEngine { get; }
        public GameDescriptions Descriptions { get; }
        
        public BuildDialogModel BuildDialogModel { get; }
        public BuildingsStatisticModel StatisticModel { get; }
        public CosmicShipModel ShipModel { get; set; }
        public EarthGridModel EarthGridModel { get; set; }
        public EarthGridExpansionModel ExpansionModel { get; set; }
        
        public GameManager(CoreStartView coreStartView, GameDescriptions descriptions, SceneLoaderPresenter sceneLoader, SystemsEngine systemEngine, BuildDialogModel buildDialogModel, BuildingsStatisticModel statisticModel)
        {
            Descriptions = descriptions;
            CoreStartView = coreStartView;
            SceneLoader = sceneLoader;
            SystemEngine = systemEngine;
            BuildDialogModel = buildDialogModel;
            StatisticModel = statisticModel;
        }
    }
}