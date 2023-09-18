using Dialogs.BuildDialog;
using Utilities;

namespace BuildingsStatistic
{
    public class BuildingsStatisticPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildingsStatisticModel _model;

        public BuildingsStatisticPresenter(GameManager manager, BuildingsStatisticModel model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            // _model.OnLimitUpdated -= UpdateLimits;
            _model.OnBuildingRegistered -= UpdateLimits;
        }

        public void Activate()
        {
            // _model.OnLimitUpdated += UpdateLimits;
            _model.OnBuildingRegistered += UpdateLimits;
        }

        private void UpdateLimits(string buildingId)
        {
            _model.BuildingLimits[buildingId] -= _model.BuildingLimits[buildingId] == 0 ? 0 : 1;
            
            var neededSpecification = _manager.Specifications.Buildings.Find(item => item.Id == buildingId);
            var neededCategory = _manager.DialogsModel.GetByType<BuildDialogModel>().CategoriesModels.Find(item => item.Specification.Category == neededSpecification.Category);
            var neededCard = neededCategory.CardsModels.Find(item => item.Specification.Id == buildingId);
                
            neededCard.RedrawLimitText(_model.BuildingLimits[buildingId]);
        }
    }
}