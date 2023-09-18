using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Helpers
{
    public class PolygonDrawingHelper : MonoBehaviour
    {
        public bool DrawLinesForRectangle = true;
        public bool JoinLastAndFirstVertex = true;
        public bool GenerateTestTemplate;
        public List<Vector3> Positions;

        private void OnDrawGizmos()
        {
            CosmicShipFloorCalculationsHelper.DefineCellsInsideGivenArea(Positions, true, JoinLastAndFirstVertex, DrawLinesForRectangle, GenerateTestTemplate);
        }
    }
}