using System.Collections.Generic;
using System.Linq;
using Dialogs.BuildDialog;
using Dialogs.ChangeFloorDialog;
using Utilities;

namespace Dialogs.Base
{
    public class DialogsModel
    {
        private readonly List<IDialogModel> _dialogs = new();

        public DialogsModel(GameSpecifications specifications)
        {
            Add(new BuildDialogModel(specifications.Builds));
            Add(new ChangeFloorDialogModel(specifications.Floors));
        }

        private void Add(IDialogModel dialog)
        {
            _dialogs.Add(dialog);
        }

        public TType GetByType<TType>() where TType : IDialogModel
        {
            return _dialogs.OfType<TType>().First();
        }
    }
}