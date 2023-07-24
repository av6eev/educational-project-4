using Utilities;

namespace Dialogs.ChangeFloorDialog
{
    public class ChangeFloorDialogPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly ChangeFloorDialogModel _model;
        private readonly ChangeFloorDialogView _view;

        public ChangeFloorDialogPresenter(GameManager manager, ChangeFloorDialogModel model, ChangeFloorDialogView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }
        
        public void Deactivate()
        {
            _view.NextFloorBtn.onClick.RemoveListener(OnSelectNextFloor);
            _view.PreviousFloorBtn.onClick.RemoveListener(OnSelectPreviousFloor);

            _model.OnShow -= Show;
            _model.OnHide -= Hide;
        }

        public void Activate()
        {
            _view.NextFloorBtn.onClick.AddListener(OnSelectNextFloor);
            _view.PreviousFloorBtn.onClick.AddListener(OnSelectPreviousFloor);
            
            _model.OnShow += Show;
            _model.OnHide += Hide;
        }

        private void Hide()
        {
            _view.gameObject.SetActive(false);
        }

        private void Show()
        {
            _view.gameObject.SetActive(true);
        }

        private void OnSelectNextFloor()
        {
            _manager.ShipModel.ChangeFloor(1);
        }
        
        private void OnSelectPreviousFloor()
        {
            _manager.ShipModel.ChangeFloor(-1);
        }
    }
}