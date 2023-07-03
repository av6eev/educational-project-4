using UnityEngine;

namespace CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        public float ZoomBuffSpeed = 2f;
        public float LerpDragSpeed = .05f;
        public float LerpZoomSpeed = .1f;
        
        private Camera _mainCamera;
        private Vector3 _lastMousePosition;
        private bool _isDragAllow;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _lastMousePosition = Input.mousePosition;
                _isDragAllow = true;
            }
            
            if (Input.GetMouseButtonUp(1)) _isDragAllow = false;
        
            HandleDragMovement(_isDragAllow);
            HandleZoom();
        }

        private void HandleDragMovement(bool isDragAllow)
        {
            if (!isDragAllow) return;
            if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;
            
            var cachedTransform = transform;
            var cachedTransformPosition = cachedTransform.position;
            var inputDirection = Vector3.zero;
            var delta = _lastMousePosition - Input.mousePosition;
            
            inputDirection.x = delta.x;
            inputDirection.z = delta.y;
            
            var moveDirection = cachedTransform.forward * inputDirection.z + cachedTransform.right * inputDirection.x;
            var targetPosition = cachedTransformPosition + moveDirection;
            
            transform.position = Vector3.Lerp(cachedTransformPosition, targetPosition, LerpDragSpeed);
        }
        
        private void HandleZoom()
        {
            var cachedTransformPosition = transform.position;
            var desiredPosition = Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition) , out var hit) ? hit.point : cachedTransformPosition;
            var distance = Vector3.Distance(desiredPosition , cachedTransformPosition);
            var direction = Vector3.Normalize(desiredPosition - cachedTransformPosition) * (distance * Input.GetAxis("Mouse ScrollWheel") * ZoomBuffSpeed);
            var targetPosition = cachedTransformPosition + direction;
            
            if (distance < 1f && direction.y < 0) return;

            transform.position = Vector3.Lerp(cachedTransformPosition, targetPosition, LerpZoomSpeed);
        }
    }
}