using BuildingsStatistic;
using Core;
using Cosmic.Ship;
using Dialogs.Base;
using Earth.Grid;
using Earth.Grid.Expansion;

namespace Utilities
{
    public class GameManager
    {
        public CoreStartView CoreStartView { get; }
        public SceneLoaderPresenter SceneLoader { get; }
        public SystemsEngine SystemEngine { get; }
        public GameDescriptions Descriptions { get; }
        
        public DialogsModel DialogsModel { get; }
        public BuildingsStatisticModel StatisticModel { get; }
        public CosmicShipModel ShipModel { get; set; }
        public EarthGridModel EarthGridModel { get; set; }
        public EarthGridExpansionModel ExpansionModel { get; set; }
        
        public GameManager(CoreStartView coreStartView, GameDescriptions descriptions, SceneLoaderPresenter sceneLoader, SystemsEngine systemEngine, DialogsModel dialogsModel, BuildingsStatisticModel statisticModel)
        {
            Descriptions = descriptions;
            CoreStartView = coreStartView;
            SceneLoader = sceneLoader;
            SystemEngine = systemEngine;
            DialogsModel = dialogsModel;
            StatisticModel = statisticModel;
        }
    }
}