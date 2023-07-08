using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingDialog
{
    public class BuildingDialogView : MonoBehaviour
    {
        public TextMeshProUGUI DescriptionTxt;
        public TextMeshProUGUI CurrentLvlTxt;
        public Button UpgradeLevelBtn;
        public Image PreviewImage;

        private void Start()
        {
            var parent = GameObject.Find("BuildingDialogRoot").transform;
            
            transform.position = parent.position;
            transform.SetParent(parent);
        }
    }
}