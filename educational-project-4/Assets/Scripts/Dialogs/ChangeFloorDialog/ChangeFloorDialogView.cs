using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.ChangeFloorDialog
{
    public class ChangeFloorDialogView : MonoBehaviour
    {
        [field: SerializeField] public Button NextFloorBtn { get; private set; }
        [field: SerializeField] public Button PreviousFloorBtn { get; private set; }
    }
}