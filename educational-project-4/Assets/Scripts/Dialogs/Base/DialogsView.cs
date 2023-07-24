using Dialogs.BuildDialog;
using Dialogs.ChangeFloorDialog;
using UnityEngine;

namespace Dialogs.Base
{
    public class DialogsView : MonoBehaviour
    {
        [field: SerializeField] public BuildDialogView BuildDialogView {get; private set;}
        [field: SerializeField] public ChangeFloorDialogView ChangeFloorDialogView {get; private set;}
    }
}