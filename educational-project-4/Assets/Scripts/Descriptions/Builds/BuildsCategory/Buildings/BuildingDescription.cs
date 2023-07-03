using System;
using Building;
using Descriptions.Base;
using UnityEngine;

namespace Descriptions.Builds.BuildsCategory.Buildings
{
    [Serializable]
    public class BuildingDescription : IDescription
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
    }
}