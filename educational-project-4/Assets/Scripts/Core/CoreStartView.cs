using BuildDialog;
using Descriptions.Base;
using UnityEngine;

namespace Core
{
    public class CoreStartView : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera {get; private set;}
        [field: SerializeField] public GameObject MainUIRoot {get; private set;}
        [field: SerializeField] public BuildDialogView BuildDialogView {get; private set;}
        [field: SerializeField] public DescriptionsCollectionSo DescriptionsCollection {get; private set;}
    }
}