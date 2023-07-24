using System;
using System.Collections.Generic;
using BuildDialog.BuildCategoryDialog.BuildCardDialog;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BuildDialog.BuildCategoryDialog
{
    public class BuildCategoryDialogView : MonoBehaviour, IPointerDownHandler
    {
        public event Action OnMouseDown;
        
        [field: SerializeField] public TextMeshProUGUI TitleTxt { get; private set; }
        [field: SerializeField] public BuildCardDialogView BuildCardPrefab { get; private set; }

        private readonly List<BuildCardDialogView> _buildings = new();
        
        public BuildCardDialogView InstantiateBuildingCard(RectTransform contentRoot, string title, string limit, Sprite image)
        {
            var view = Instantiate(BuildCardPrefab, contentRoot);
            view.SetupCard(limit, title, image);
            
            _buildings.Add(view);
            return view;
        }
        
        public void ClearBuildingCards()
        {
            foreach (var view in _buildings)
            {
                Destroy(view.gameObject);
            }
            
            _buildings.Clear();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnMouseDown?.Invoke();    
        }
    }
}