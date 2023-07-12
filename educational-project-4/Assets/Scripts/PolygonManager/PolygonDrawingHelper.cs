using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PolygonManager
{
    public class PolygonDrawingHelper : MonoBehaviour
    {
        public GameObject Cell;
        public List<Vector2> Positions;

        private readonly Dictionary<Vector3, GameObject> _cells = new();

        public void Start()
        {
            var cellsToRemove = new List<KeyValuePair<Vector3, GameObject>>();
            
            GenerateCellsInsideGivenArea();
            
            foreach (var cell in _cells.Where(cell => !IsInPolygon(cell.Key, Positions)))
            {
                cellsToRemove.Add(cell);
                Destroy(cell.Value.gameObject);
            }

            foreach (var cell in cellsToRemove)
            {
                _cells.Remove(cell.Key);
            }
        }

        private void GenerateCellsInsideGivenArea()
        {
            if (Positions.Count == 0) return;
            
            var leftUpCorner = Vector3.zero;
            var rightUpCorner = Vector3.zero;

            var (xMaxSide, xMinSide) = FindSides(Positions);
            var xMinRightPoint = xMinSide.Last();
            var xMinLeftPoint = xMinSide.First();
            var xMaxRightPoint = xMaxSide.First();
            var xMaxLeftPoint = xMaxSide.Last();
            
            var distanceToRightPoint = Mathf.Sqrt((float)Math.Pow(xMinRightPoint.z - FindRightPoint().y, 2));
            var distanceToLeftPoint = Mathf.Sqrt((float)Math.Pow(xMinLeftPoint.z - FindLeftPoint().y, 2));

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
                for (var z = rightDownCorner.z; z < Vector3.Distance(leftUpCorner, rightUpCorner) + rightDownCorner.z; z++)
                {
                    var position = new Vector3(x + .5f, 0, z + .5f);
                    var cell = Instantiate(Cell, position, Quaternion.identity);
                    _cells.Add(position, cell);
                }
            }
        }

        private bool IsInPolygon(Vector3 cell, List<Vector2> polygon)
        {
            var intersects = new List<float>();
            var a = polygon.Last();
            
            foreach (var b in polygon)
            {
                if ((b.y < cell.z && a.y >= cell.z) || (a.y < cell.z && b.y >= cell.z))
                {
                    var px = (int)(b.x + .5f * (cell.z - b.y) / (a.y - b.y) * (a.x - b.x));
                    intersects.Add(px);
                }

                a = b;
            }

            intersects.Sort();
            
            return intersects.IndexOf(cell.x) % 2 == 0 || intersects.Count(x => x < cell.x) % 2 == 1;
        }

        private void OnDrawGizmos()
        {
            if (Positions.Count == 0) return;

            for (var i = 1; i < Positions.Count; i++)
            {
                Gizmos.DrawLine(new Vector3(Positions[i - 1].x, 0, Positions[i - 1].y), new Vector3(Positions[i].x, 0, Positions[i].y));
            }

            Gizmos.DrawLine(new Vector3(Positions[0].x, 0, Positions[0].y), new Vector3(Positions[^1].x, 0, Positions[^1].y));
            
            var (xMaxSide, xMinSide) = FindSides(Positions);
            var xMinRightPoint = xMinSide.Last();
            var xMinLeftPoint = xMinSide.First();
            var xMaxRightPoint = xMaxSide.First();
            var xMaxLeftPoint = xMaxSide.Last();
            
            var distanceToRightPoint = Mathf.Sqrt((float)Math.Pow(xMinRightPoint.z - FindRightPoint().y, 2));
            var distanceToLeftPoint = Mathf.Sqrt((float)Math.Pow(xMinLeftPoint.z - FindLeftPoint().y, 2));

            Debug.DrawRay(xMaxRightPoint, new Vector3(0, 0, -distanceToRightPoint));
            Debug.DrawRay(xMinLeftPoint, new Vector3(0, 0, distanceToLeftPoint));
            
            if (xMinLeftPoint.z.Equals(xMaxLeftPoint.z))
            {
                Debug.DrawRay(xMaxLeftPoint, new Vector3(0, 0, distanceToLeftPoint));
            }

            if (xMinRightPoint.z.Equals(xMaxRightPoint.z))
            {
                Debug.DrawRay(xMinRightPoint, new Vector3(0, 0, -distanceToRightPoint));
            }
        }

        private Vector2 FindRightPoint()
        {
            var maxX = Positions[0].x;
            var minZ = Positions[0].y;

            minZ = Positions.Select(position => position.y).Prepend(minZ).Min();

            foreach (var position in Positions.Where(position => position.y.Equals(minZ) && position.x > maxX))
            {
                maxX = position.x;
            }

            return new Vector2(maxX, minZ);
        }
        
        private Vector2 FindLeftPoint()
        {
            var maxX = Positions[0].x;
            var maxZ = Positions[0].y;

            maxZ = Positions.Select(position => position.y).Prepend(maxZ).Max();

            foreach (var position in Positions.Where(position => position.y.Equals(maxZ) && position.x > maxX))
            {
                maxX = position.x;
            }

            return new Vector2(maxX, maxZ);
        }

        private Vector3 CalculateSum(Vector3 point, float distance, Vector3 direction)
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
        
        private (List<Vector3>, List<Vector3>) FindSides(List<Vector2> positions)
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
            
            foreach (var position in Positions)
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