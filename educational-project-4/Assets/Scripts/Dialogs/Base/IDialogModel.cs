using System;
using Specifications.Base;

namespace Dialogs.Base
{
    public interface IDialogModel
    {
        event Action OnShow, OnHide;
        ISpecification Specification { get; }
        void Show();
        void Hide();
    }
}