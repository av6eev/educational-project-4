using Dialogs.BuildDialog;
using Earth.Grid;
using UnityEngine;

namespace Earth
{
    public class EarthView : MonoBehaviour
    {
        [field: SerializeField] public EarthGridView EarthGridView { get; private set; }
        [field: SerializeField] public BuildDialogView BuildDialogView { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
    }
}