using Dialogs.Base;
using Specifications.Base;
using UnityEngine;

namespace Core
{
    public class CoreStartView : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public GameObject MainUIRoot { get; private set; }
        [field: SerializeField] public DialogsView DialogsView { get; private set; }
        [field: SerializeField] public SpecificationsCollectionSo SpecificationsCollection { get; private set; }
    }
}