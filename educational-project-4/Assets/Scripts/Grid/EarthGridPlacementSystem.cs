using Earth;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Grid
{
    public class EarthGridPlacementSystem : ISystem
    {
        private readonly EarthLocationManager _manager;
        private readonly GridView _gridView;

        public EarthGridPlacementSystem(EarthLocationManager manager)
        {
            _manager = manager;
            _gridView = _manager.EarthSceneView.EarthView.GridView;
        }
        
        public void Update(float deltaTime)
        {
            var gridModel = _manager.GridModel;
            var mousePosition = GetSelectedPosition(_manager.EarthSceneView.EarthView.Camera, _gridView);
            var gridPosition = _gridView.Grid.WorldToCell(mousePosition);
            
            if (gridModel.LastDetectedPosition == gridPosition) return;
            if (gridModel.LastSelectedBuilding == null) return;

            _gridView.MouseIndicator.transform.position = mousePosition;
            var isValid = gridModel.IsPlacementValid(gridPosition, _manager.ExpansionModel.CurrentGridSize);
            
            UpdatePreviewPosition(gridPosition, isValid);
            HandleInput(gridModel, isValid);
        }

        private void HandleInput(GridModel model, bool isValid)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                model.ChangePlacementMode(false);
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && model.LastSelectedBuilding != null && isValid)
            {
                model.PlaceBuilding();
            }
        }
        
        private void UpdatePreviewPosition(Vector3 position, bool isValid)
        {
            var color = isValid ? Color.green : Color.red;
            
            color.a = .5f;
            _gridView.CellSelectedIndicatorRenderer.color = color;
            _gridView.PreviewMaterialInstance.color = color;

            _gridView.CellSelectedIndicator.transform.position = Vector3.Lerp(_gridView.CellSelectedIndicator.transform.position, position, .2f);
            _gridView.PreviewObject.transform.position = Vector3.Lerp(_gridView.PreviewObject.transform.position, new Vector3(position.x, position.y + .06f, position.z), .2f);
        }

        private Vector3 GetSelectedPosition(Camera camera, GridView gridView)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = camera.nearClipPlane;
            
            if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out var hit, 3000, gridView.PlacementMask))
            {
                _manager.GridModel.LastPosition = hit.point;
                // _manager.EarthView.FogParticleSystem.SetActive(hit.distance > 20f);
            }

            return _manager.GridModel.LastPosition;
        }
    }
}