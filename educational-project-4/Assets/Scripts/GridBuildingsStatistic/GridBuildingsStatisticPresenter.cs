using Earth;
using Utilities;

namespace GridBuildingsStatistic
{
    public class GridBuildingsStatisticPresenter : IPresenter
    {
        private readonly EarthLocationManager _manager;
        private readonly GridBuildingsStatisticModel _model;

        public GridBuildingsStatisticPresenter(EarthLocationManager manager, GridBuildingsStatisticModel model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            _model.OnLimitUpdated -= UpdateLimits;
        }

        public void Activate()
        {
            _model.OnLimitUpdated += UpdateLimits;
        }

        private void UpdateLimits(string buildingId)
        {
            var neededDescription = _manager.Descriptions.Buildings.Find(item => item.Id == buildingId);
            var neededCategory = _manager.BuildDialogModel.CategoriesModels.Find(item => item.Description.Category == neededDescription.Category);
            var neededCard = neededCategory.CardsModels.Find(item => item.Description.Id == buildingId);
                
            neededCard.RedrawLimitText(_model.BuildingLimits[buildingId]);
        }
    }
}