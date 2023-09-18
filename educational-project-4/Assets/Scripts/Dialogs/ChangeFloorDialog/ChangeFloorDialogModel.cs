using System;
using Dialogs.Base;
using Specifications.Base;

namespace Dialogs.ChangeFloorDialog
{
    public class ChangeFloorDialogModel : IDialogModel
    {
        public event Action OnShow, OnHide;
        public ISpecification Specification { get; }

        public ChangeFloorDialogModel(ISpecification specification)
        {
            Specification = specification;
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