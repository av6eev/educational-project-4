using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Helpers;

namespace Cosmic
{
    public class CosmicView : MonoBehaviour
    {
        [field: SerializeField] public List<PolygonDrawingHelper> DrawingHelper { get; private set; }
        [field: NonSerialized] public Dictionary<int, List<Vector3>> LevelsPositions { get; } = new();

        private void Awake()
        {
            for (var index = 0; index < DrawingHelper.Count; index++)
            {
                LevelsPositions.Add(index, DrawingHelper[index].Positions);
            }
        }
    }
}