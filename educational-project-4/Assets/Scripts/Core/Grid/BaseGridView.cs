using System;
using Building;
using UnityEngine;

namespace Core.Grid
{
    public class BaseGridView : MonoBehaviour
    {
        public GameObject GridRoot;
        public GameObject CellSelectedIndicator;
        
        [NonSerialized] public Material PreviewMaterialInstance;
        public Material PreviewMaterial;
        public LayerMask PlacementMask;
        
        [NonSerialized] public SpriteRenderer CellSelectedIndicatorRenderer;
        [NonSerialized] public BuildingView PreviewObject; 
        
        public virtual void CreatePreview(BuildingView prefab)
        {
            PreviewObject = Instantiate(prefab);
        }

        public virtual void DestroyPreview()
        {
            CellSelectedIndicator.SetActive(false);
            Destroy(PreviewObject.gameObject);
            PreviewObject = null;
        }
    }
}