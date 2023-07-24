using System;
using Building;
using UnityEngine;

namespace Core.Grid
{
    public class BaseGridView : MonoBehaviour
    {
        [field: SerializeField] public GameObject GridRoot { get; private set; }
        [field: SerializeField] public GameObject CellSelectedIndicator { get; private set; } 
        [field: SerializeField] public Material PreviewMaterial { get; private set; }
        [field: SerializeField] public LayerMask PlacementMask { get; private set; }
        
        [field: NonSerialized] public Material PreviewMaterialInstance { get; private set; }
        [field: NonSerialized] public SpriteRenderer CellSelectedIndicatorRenderer { get; private set; }
        [field: NonSerialized] public BuildingView PreviewObject { get; private set; } 
        
        private void Start()
        {
            CellSelectedIndicatorRenderer = CellSelectedIndicator.GetComponentInChildren<SpriteRenderer>();
            PreviewMaterialInstance = new Material(PreviewMaterial);
        }
        
        public virtual void CreatePreview(BuildingView prefab)
        {
            PreviewObject = Instantiate(prefab);
            PreviewObject.gameObject.layer = 2;
        }

        public virtual void DestroyPreview()
        {
            CellSelectedIndicator.SetActive(false);
            Destroy(PreviewObject.gameObject);
            PreviewObject = null;
        }
    }
}