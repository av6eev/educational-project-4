using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Grid
{
    public class GridPlacementSystem : ISystem
    {
        private readonly GameManager _manager;
        private readonly GridView _gridView;

        public GridPlacementSystem(GameManager manager)
        {
            _manager = manager;
            _gridView = _manager.GameView.GridView;
        }
        
        public void Update(float deltaTime)
        {
            var gridModel = _manager.GridModel;
            var mousePosition = GetSelectedPosition(_manager.GameView.Camera, _gridView);
            var gridPosition = _gridView.Grid.WorldToCell(mousePosition);
            
            HandleInput(gridModel);
            
            if (gridModel.LastDetectedPosition == gridPosition) return;
            if (gridModel.LastSelectedBuilding == null) return;

            _gridView.MouseIndicator.transform.position = mousePosition;
            UpdatePreviewPosition(gridPosition, gridModel.IsPlacementValid(gridPosition));
        }

        private void HandleInput(GridModel model)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                model.ChangePlacementMode(false);
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && model.LastSelectedBuilding != null)
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
            
            if (Physics.Raycast(camera.ScreenPointToRay(mousePosition), out var hit, 500, gridView.PlacementMask))
            {
                _manager.GridModel.LastPosition = hit.point;
                // _manager.GameView.FogParticleSystem.SetActive(hit.distance > 20f);
            }

            return _manager.GridModel.LastPosition;
        }
    }
}