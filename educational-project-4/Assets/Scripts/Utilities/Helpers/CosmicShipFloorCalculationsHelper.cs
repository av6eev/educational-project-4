using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class CosmicShipFloorCalculationsHelper
    {
        public static List<Vector2Int> DefineCellsInsideGivenArea(List<Vector2> positions)
        {
            if (positions.Count == 0) return null;

            var returnCells = new List<Vector2Int>();
            
            var leftUpCorner = Vector3.zero;
            var rightUpCorner = Vector3.zero;

            var (xMaxSide, xMinSide) = FindSides(positions);
            var xMinRightPoint = xMinSide.Last();
            var xMinLeftPoint = xMinSide.First();
            var xMaxRightPoint = xMaxSide.First();
            var xMaxLeftPoint = xMaxSide.Last();
            
            var distanceToRightPoint = Mathf.Sqrt((float)Math.Pow(xMinRightPoint.z - FindRightPoint(positions).y, 2));
            var distanceToLeftPoint = Mathf.Sqrt((float)Math.Pow(xMinLeftPoint.z - FindLeftPoint(positions).y, 2));

            var leftDownCorner = CalculateSum(xMinLeftPoint, distanceToLeftPoint, Vector3.forward);
            
            if (xMinLeftPoint.z.Equals(xMaxLeftPoint.z))
            {
                leftUpCorner = CalculateSum(xMaxLeftPoint, distanceToLeftPoint, Vector3.forward);
            }

            var rightDownCorner = CalculateSum(xMinRightPoint, distanceToRightPoint, Vector3.back);

            if (xMinRightPoint.z.Equals(xMaxRightPoint.z))
            {
                rightUpCorner = CalculateSum(xMaxRightPoint, distanceToRightPoint, Vector3.back);
            }
            
            for (var x = rightDownCorner.x; x < Vector3.Distance(leftUpCorner, leftDownCorner) + rightDownCorner.x; x++)
            {
                for (var z = rightDownCorner.z + 1; z < Vector3.Distance(leftUpCorner, rightUpCorner) + rightDownCorner.z + 1; z++)
                {
                    var position = new Vector2Int((int)x, (int)z);
                    returnCells.Add(position);
                }
            }

            var cellsToRemove = returnCells.Where(cell => !IsInPolygon(cell, positions)).ToList();
            
            foreach (var cell in cellsToRemove)
            {
                returnCells.Remove(cell);
            }

            return returnCells;
        }
        
        private static bool IsInPolygon(Vector2 cell, List<Vector2> polygon)
        {
            var intersects = new List<float>();
            var a = polygon.Last();
            
            foreach (var b in polygon)
            {
                if ((b.y < cell.y && a.y >= cell.y) || (a.y < cell.y && b.y >= cell.y))
                {
                    var px = (int)(b.x + .5f * (cell.y - b.y) / (a.y - b.y) * (a.x - b.x));
                    intersects.Add(px);
                }

                a = b;
            }

            intersects.Sort();

            var count = 0;
            
            foreach (var x in intersects)
            {
                if (x <= cell.x) count++;
            }

            return intersects.IndexOf(cell.x) % 2 == 0 || count % 2 == 1;
        }

        private static Vector2 FindRightPoint(List<Vector2> positions)
        {
            var maxX = positions[0].x;
            var minZ = positions[0].y;

            minZ = positions.Select(position => position.y).Prepend(minZ).Min();

            foreach (var position in positions.Where(position => position.y.Equals(minZ) && position.x > maxX))
            {
                maxX = position.x;
            }

            return new Vector2(maxX, minZ);
        }
        
        private static Vector2 FindLeftPoint(List<Vector2> positions)
        {
            var maxX = positions[0].x;
            var maxZ = positions[0].y;

            maxZ = positions.Select(position => position.y).Prepend(maxZ).Max();

            foreach (var position in positions.Where(position => position.y.Equals(maxZ) && position.x > maxX))
            {
                maxX = position.x;
            }

            return new Vector2(maxX, maxZ);
        }

        private static Vector3 CalculateSum(Vector3 point, float distance, Vector3 direction)
        {
            var result = Vector3.zero;
            
            result.x = point.x;
            
            if (direction == Vector3.forward)
            {
                result.z = point.z + distance;
            }

            if (direction == Vector3.back)
            {
                result.z = point.z - distance;
            }

            return result;
        }
        
        private static (List<Vector3>, List<Vector3>) FindSides(List<Vector2> positions)
        {
            var minX = positions[0].x;
            var maxX = positions[0].x;

            var side1 = new List<Vector3>();
            var side2 = new List<Vector3>();
            
            for (var i = 0; i < positions.Count; i++)
            {
                if (positions[i].x < minX)
                {
                    minX = positions[i].x;
                }

                if (positions[i].x > maxX)
                {
                    maxX = positions[i].x;
                }
            }
            
            foreach (var position in positions)
            {
                if (position.x.Equals(maxX))
                {
                    side1.Add(new Vector3(position.x, 0, position.y));
                }

                if (position.x.Equals(minX))
                {
                    side2.Add(new Vector3(position.x, 0, position.y));
                }
            }

            return (side1, side2);
        }
    }
}