using System;
using Building;
using UnityEngine;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        public GameObject GridRoot;
        public GameObject CellSelectedIndicator;
        public GameObject MouseIndicator;
        
        [NonSerialized] public BuildingView PreviewObject; 
        
        [NonSerialized] public Material PreviewMaterialInstance;
        public Material PreviewMaterial;
        
        public UnityEngine.Grid Grid;
        public LayerMask PlacementMask;
        
        [NonSerialized] public SpriteRenderer CellSelectedIndicatorRenderer;

        private void Start()
        {
            CellSelectedIndicatorRenderer = CellSelectedIndicator.GetComponentInChildren<SpriteRenderer>();
            PreviewMaterialInstance = new Material(PreviewMaterial);
        }

        public (Vector3, BuildingView) CreateBuilding(BuildingView prefab, Vector3Int gridPosition)
        {
            var building = Instantiate(prefab);
            var result = (Vector3: building.transform.position = Grid.CellToWorld(gridPosition), BuildingView: building);
            return result;
        }

        public void CreatePreview(BuildingView prefab)
        {
            PreviewObject = Instantiate(prefab);
        }

        public void DestroyPreview()
        {
            CellSelectedIndicator.SetActive(false);
            Destroy(PreviewObject.gameObject);
            PreviewObject = null;
        }
    }
}