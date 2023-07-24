using System;
using Descriptions.Base;

namespace Dialogs.Base
{
    public interface IDialogModel
    {
        event Action OnShow, OnHide;
        IDescription Description { get; }
        void Show();
        void Hide();
    }
}