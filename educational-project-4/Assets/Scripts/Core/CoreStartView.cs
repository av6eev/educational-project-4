using Descriptions.Base;
using Dialogs.Base;
using UnityEngine;

namespace Core
{
    public class CoreStartView : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public GameObject MainUIRoot { get; private set; }
        [field: SerializeField] public DialogsView DialogsView { get; private set; }
        [field: SerializeField] public DescriptionsCollectionSo DescriptionsCollection { get; private set; }
    }
}