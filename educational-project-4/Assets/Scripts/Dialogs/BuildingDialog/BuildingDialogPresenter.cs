using Utilities;

namespace Dialogs.BuildingDialog
{
    public class BuildingDialogPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly BuildingDialogModel _model;
        private readonly BuildingDialogView _view;

        public BuildingDialogPresenter(GameManager manager, BuildingDialogModel model, BuildingDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.UpgradeLevelBtn.onClick.RemoveListener(OnClick);
        }

        public void Activate()
        {
            _view.UpgradeLevelBtn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            var buildingModel = _manager.StatisticModel.Buildings.Find(item => item.Specification.Category == _model.Specification.Category);

            buildingModel.UpdateLevelUpgrade();
            _view.CurrentLvlTxt.text = buildingModel.CurrentUpgradeLevel.ToString();
        }
    }
}