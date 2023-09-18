using System;
using Building;
using Specifications.Base;
using UnityEngine;

namespace Specifications.Builds.Buildings
{
    [Serializable]
    public class BuildingSpecification : ISpecification
    {
        [Header("General")] 
        public string Id; 
        public string Title;
        public string Category;
        public int Priority;
        public int Limit;

        [Header("Dialog")] 
        public string DialogId;
        
        [Header("Others")]
        public Vector2Int Size = Vector2Int.one;
        public BuildingView Prefab;
        public Sprite PreviewImage;
    }
}