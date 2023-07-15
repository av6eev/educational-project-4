using UnityEngine;

namespace Utilities
{
    public abstract class BaseGridPlacementSystem
    {
        public virtual Vector3 GetMouseWorldPosition()
        {
            return Physics.Raycast(Camera.main!.ScreenPointToRay(Input.mousePosition), out var hit, 1000f) ? hit.point : Vector3.zero;
        }
    }
}