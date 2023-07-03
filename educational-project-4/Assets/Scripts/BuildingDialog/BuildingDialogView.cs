using TMPro;
using UnityEngine;

namespace BuildingDialog
{
    public class BuildingDialogView : MonoBehaviour
    {
        public TextMeshProUGUI DescriptionTxt;

        private void Start()
        {
            var parent = GameObject.Find("BuildingDialogRoot").transform;
            
            transform.position = parent.position;
            transform.SetParent(parent);
        }
    }
}