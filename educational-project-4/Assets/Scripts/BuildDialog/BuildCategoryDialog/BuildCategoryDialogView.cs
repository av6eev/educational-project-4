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
        public event Action Down;
        
        public TextMeshProUGUI TitleTxt;
        public BuildCardDialogView BuildingCellPrefab;

        private readonly List<BuildCardDialogView> _buildings = new();
        
        public BuildCardDialogView InstantiateBuildingCard(RectTransform contentRoot, string title, string limit)
        {
            var view = Instantiate(BuildingCellPrefab, contentRoot);
            view.LimitTxt.text = limit;
            view.TitleTxt.text = title;
            
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
            Down?.Invoke();    
        }
    }
}