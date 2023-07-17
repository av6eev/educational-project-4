using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Earth.Grid
{
    public class EarthGridPlacementSystem : ISystem
    {
        private readonly EarthLocationManager _manager;
        private readonly EarthGridView _earthGridView;

        public EarthGridPlacementSystem(EarthLocationManager manager)
        {
            _manager = manager;
            _earthGridView = _manager.EarthSceneView.EarthView.EarthGridView;
        }
        
        public void Update(float deltaTime)
        {
            var gridModel = _manager.EarthGridModel;
            var mousePosition = GetSelectedPosition(_manager.EarthSceneView.EarthView.Camera, _earthGridView);
            var gridPosition = _earthGridView.Grid.WorldToCell(mousePosition);
            
            if (gridModel.LastDetectedPosition == gridPosition) return;
            if (gridModel.LastSelectedBuilding == null) return;

            var isValid = gridModel.IsPlacementValid(gridPosition, _manager.ExpansionModel.CurrentGridSize);
            
            UpdatePreviewPosition(gridPosition, isValid);
            HandleInput(gridModel, isValid);
        }

        private void HandleInput(EarthGridModel model, bool isValid)
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
            _earthGridView.CellSelectedIndicatorRenderer.color = color;
            _earthGridView.PreviewMaterialInstance.color = color;

            _earthGridView.CellSelectedIndicator.transform.position = Vector3.Lerp(_earthGridView.CellSelectedIndicator.transform.position, position, .2f);
            _earthGridView.PreviewObject.transform.position = Vector3.Lerp(_earthGridView.PreviewObject.transform.position, new Vector3(position.x, position.y + .06f, position.z), .2f);
        }

        private Vector3 GetSelectedPosition(Camera camera, EarthGridView earthGridView)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = camera.nearClipPlane;
            
            if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out var hit, 3000, earthGridView.PlacementMask))
            {
                _manager.EarthGridModel.LastPosition = hit.point;
                // _manager.EarthView.FogParticleSystem.SetActive(hit.distance > 20f);
            }

            return _manager.EarthGridModel.LastPosition;
        }
    }
}