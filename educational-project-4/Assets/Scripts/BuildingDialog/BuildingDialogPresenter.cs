using Utilities;

namespace BuildingDialog
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
            
        }

        public void Activate()
        {
            
        }
    }
}