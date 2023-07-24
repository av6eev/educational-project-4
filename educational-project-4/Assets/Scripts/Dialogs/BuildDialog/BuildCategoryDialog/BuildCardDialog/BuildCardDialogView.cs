using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialogs.BuildDialog.BuildCategoryDialog.BuildCardDialog
{
    public class BuildCardDialogView : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnMouseDown;

        [field: SerializeField] public TextMeshProUGUI TitleTxt { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LimitTxt { get; private set; }
        [field: SerializeField] public Image PreviewImage { get; private set; }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnMouseDown?.Invoke();   
        }

        public void SetupCard(string limit, string title, Sprite image)
        {
            LimitTxt.text = limit;
            TitleTxt.text = title;
            
            if (image != null)
            {
                PreviewImage.sprite = image;
            }
            else
            {
                PreviewImage.gameObject.SetActive(false);
            }
        }
    }
}