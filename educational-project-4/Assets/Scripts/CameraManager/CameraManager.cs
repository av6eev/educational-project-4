using System;
using Cinemachine;
using UnityEngine;

namespace CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        public float ZoomBuffSpeed = 2f;
        public float LerpDragSpeed = .05f;
        public float LerpZoomSpeed = .1f;
        public float FollowOffsetMin = 5f;
        public float FollowOffsetMax = 17f;
        public float TimeToSmoothHeightToLimit = 15f;
        public Vector3 OffsetToTarget = new(1, 0, -1);

        public CinemachineVirtualCamera VirtualCamera;
        private Camera _mainCamera;

        private Vector3 _followOffset;
        private Vector2 _lastMousePosition;
        private bool _isDragAllow;

        private void Awake()
        {
            _followOffset = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
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
            var delta = _lastMousePosition - (Vector2)Input.mousePosition;

            var inputDirection = new Vector3
            {
                x = delta.x * .1f,
                y = 0,
                z = delta.y * .1f
            };

            var moveDirection = cachedTransform.forward * inputDirection.z + cachedTransform.right * inputDirection.x;
            moveDirection.y = 0;
            var targetPosition = cachedTransformPosition + moveDirection;
            
            Debug.Log("input " + inputDirection);
            Debug.Log("direction " + moveDirection);
            
            transform.position = Vector3.Lerp(cachedTransformPosition, targetPosition, LerpDragSpeed);
            VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = _followOffset.y;
        }
        
        private void HandleZoom()
        {
            if (Input.mouseScrollDelta == Vector2.zero) return;
            
            var cachedTransformPosition = transform.position;
            var offsetToTarget = OffsetToTarget;
            
            var desiredPosition = Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition) , out var hit) ? hit.point : cachedTransformPosition;
            var distance = Vector3.Distance(desiredPosition , cachedTransformPosition);
            var direction = Vector3.Normalize(desiredPosition - cachedTransformPosition) * ((distance / 2) * Input.GetAxis("Mouse ScrollWheel") * ZoomBuffSpeed);

            if (Input.mouseScrollDelta.y > 0)
            {
                _followOffset -= direction;
            }
            else
            {
                direction = new Vector3(0, direction.y, 0);
                offsetToTarget = Vector3.zero;
                _followOffset += direction;
            }

            if (_followOffset.magnitude < FollowOffsetMin)
            {
                _followOffset = direction * FollowOffsetMin;
            }
            
            if (_followOffset.magnitude > FollowOffsetMax)
            {
                _followOffset = direction * FollowOffsetMax;
            }
            
            if (cachedTransformPosition.y >= FollowOffsetMax - .5 && Input.mouseScrollDelta.y < 0)
            {
                _followOffset = Vector3.zero;
                offsetToTarget = Vector3.zero;
            }

            if (cachedTransformPosition.y <= FollowOffsetMin + .5 && Input.mouseScrollDelta.y > 0)
            {
                _followOffset = Vector3.zero;                
                offsetToTarget = Vector3.zero;
            }
            
            var targetPosition = cachedTransformPosition + _followOffset;
            cachedTransformPosition = Vector3.Lerp(cachedTransformPosition, targetPosition - offsetToTarget, LerpZoomSpeed);

            if (transform.position.y > FollowOffsetMax && !transform.position.y.Equals(FollowOffsetMax))
            {
                cachedTransformPosition.y = Mathf.SmoothStep(cachedTransformPosition.y, FollowOffsetMax, TimeToSmoothHeightToLimit);
            }
            
            if (transform.position.y < FollowOffsetMin && !transform.position.y.Equals(FollowOffsetMin))
            {
                cachedTransformPosition.y = Mathf.SmoothStep(cachedTransformPosition.y, FollowOffsetMin, TimeToSmoothHeightToLimit);
            }

            transform.position = cachedTransformPosition;
        }
    }
}