using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogView : MonoBehaviour, IPointerDownHandler
    {
        public event Action Down;

        public TextMeshProUGUI TitleTxt;
        public TextMeshProUGUI LimitTxt;
        public Image PreviewImage;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Down?.Invoke();   
        }
    }
}