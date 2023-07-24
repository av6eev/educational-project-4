using System;
using Descriptions.Base;
using Dialogs.Base;

namespace Dialogs.ChangeFloorDialog
{
    public class ChangeFloorDialogModel : IDialogModel
    {
        public event Action OnShow, OnHide;
        public IDescription Description { get; }

        public ChangeFloorDialogModel(IDescription description)
        {
            Description = description;
        }
        
        public void Show()
        {
            OnShow?.Invoke();            
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }
    }
}