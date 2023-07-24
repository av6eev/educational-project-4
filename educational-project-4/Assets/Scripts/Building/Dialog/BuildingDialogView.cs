using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Building.Dialog
{
    public class BuildingDialogView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI DescriptionTxt { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CurrentLvlTxt { get; private set; }
        [field: SerializeField] public Button UpgradeLevelBtn { get; private set; }
        [field: SerializeField] public Image PreviewImage { get; private set; }

        private void Start()
        {
            var parent = GameObject.Find("BuildingDialogRoot").transform;
            
            transform.position = parent.position;
            transform.SetParent(parent);
        }
    }
}