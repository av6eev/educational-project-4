using Dialogs.BuildDialog;
using Dialogs.ChangeFloorDialog;
using Utilities;

namespace Dialogs.Base
{
    public class DialogsPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly DialogsModel _model;
        private readonly DialogsView _view;

        private readonly PresentersEngine _presenters = new();
        
        public DialogsPresenter(GameManager manager, DialogsModel model, DialogsView view)
        {
            _manager = manager;
            _model = model;
            _view = view;
        }

        public void Deactivate()
        {
            _presenters.Deactivate();
            _presenters.Clear();
        }

        public void Activate()
        {
            CreateNecessaryData();
            
            _presenters.Activate();
        }

        private void CreateNecessaryData()
        {
            _presenters.Add(new BuildDialogPresenter(_manager, _model.GetByType<BuildDialogModel>(), _view.BuildDialogView));
            _presenters.Add(new ChangeFloorDialogPresenter(_manager, _model.GetByType<ChangeFloorDialogModel>(), _view.ChangeFloorDialogView));
        }
    }
}