using BuildingsStatistic;
using Core;
using Cosmic.Ship;
using Dialogs.Base;
using Earth.Grid;
using Earth.Grid.Expansion;
using Save;

namespace Utilities
{
    public class GameManager
    {
        public CoreStartView CoreStartView { get; }
        public SceneLoaderPresenter SceneLoader { get; }
        public SystemsEngine SystemEngine { get; }
        public GameSpecifications Specifications { get; }

        public DialogsModel DialogsModel { get; }
        public BuildingsStatisticModel StatisticModel { get; }
        public SaveModel SaveModel { get; }
        public CosmicShipModel ShipModel { get; set; }
        public EarthGridModel EarthGridModel { get; set; }
        public EarthGridExpansionModel ExpansionModel { get; set; }
        
        public GameManager(CoreStartView coreStartView, GameSpecifications specifications, SceneLoaderPresenter sceneLoader, SystemsEngine systemEngine, DialogsModel dialogsModel, BuildingsStatisticModel statisticModel, SaveModel saveModel)
        {
            Specifications = specifications;
            CoreStartView = coreStartView;
            SceneLoader = sceneLoader;
            SystemEngine = systemEngine;
            DialogsModel = dialogsModel;
            StatisticModel = statisticModel;
            SaveModel = saveModel;
        }
    }
}