using System;
using BuildingDialog;
using UnityEngine;

namespace Building
{
    public class BuildingView : MonoBehaviour
    {
        public event Action OnClick;
        public BuildingDialogView DialogPrefab;

        private void OnMouseUpAsButton()
        {
            OnClick?.Invoke();
        }

        public void DestroyDialog(BuildingDialogView currentView)
        {
            Destroy(currentView.gameObject);
        }

        public BuildingDialogView InstantiateDialogView(string descriptionText, int currentLevel, Sprite image)
        {
            var view = Instantiate(DialogPrefab);

            view.DescriptionTxt.text = descriptionText;
            view.CurrentLvlTxt.text = currentLevel.ToString();
            
            if (image != null)
            {
                view.PreviewImage.sprite = image;
            }
            else
            {
                view.PreviewImage.gameObject.SetActive(false);
            }

            return view;
        }
    }
}