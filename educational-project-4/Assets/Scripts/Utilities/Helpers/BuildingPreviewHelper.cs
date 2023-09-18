using Core.Grid;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class BuildingPreviewHelper
    {
        public static void UpdatePreviewPosition<T>(T view, Vector3Int position, bool isValid) where T : BaseGridView
        {
            if (position == Vector3Int.zero) return;
            
            var color = isValid ? Color.green : Color.red;
            color.a = .5f;
            
            view.CellSelectedIndicatorRenderer.color = color;
            view.PreviewMaterialInstance.color = color;
            
            view.CellSelectedIndicator.transform.position = Vector3.Lerp(view.CellSelectedIndicator.transform.position, position, .2f);
            view.PreviewObject.transform.position = Vector3.Lerp(view.PreviewObject.transform.position, new Vector3(position.x, position.y + .06f, position.z), .2f);
        }
        
        public static void SettingCursor<T>(T view, Vector2Int size) where T : BaseGridView
        {
            if (size is { x: <= 0, y: <= 0 }) return;
            
            view.CellSelectedIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            view.CellSelectedIndicatorRenderer.sharedMaterial.mainTextureScale = size;
        }

        public static void SettingPreview<T>(T view) where T : BaseGridView
        {
            var renderers = view.PreviewObject.GetComponentsInChildren<Renderer>();
            
            foreach (var renderer in renderers)
            {
                var materials = renderer.materials;
                
                for (var i = 0; i < materials.Length; i++)
                {
                    materials[i] = view.PreviewMaterialInstance;
                }

                renderer.materials = materials;
            }
        }
    }
}