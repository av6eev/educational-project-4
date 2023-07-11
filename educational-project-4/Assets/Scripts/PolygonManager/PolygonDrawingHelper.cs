using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PolygonManager
{
    public class PolygonDrawingHelper : MonoBehaviour
    {
        public GameObject Prefab;
        public List<Vector2> Positions;

        private readonly Dictionary<Vector3, GameObject> _cells = new();

        public void Start()
        {
            if (Positions.Count == 0) return;
            
            var leftUpCorner = Vector3.zero;
            var leftDownCorner = Vector3.zero;
            var rightUpCorner = Vector3.zero;
            var rightDownCorner = Vector3.zero;
            
            var (side1, side2) = FindSides(Positions);
            var side2ToRightCorner = side2.Last();
            var side2ToLeftCorner = side2.First();
            var side1ToRightCorner = side1.First();
            var side1ToLeftCorner = side1.Last();
            
            var distance1 = Mathf.Sqrt((float)Math.Pow(side2ToRightCorner.z - FindRightPoint().y, 2));
            
            var distance2 = Mathf.Sqrt((float)Math.Pow(side2ToLeftCorner.z - FindLeftPoint().y, 2));
            
            leftDownCorner = CalculateSum(side2ToLeftCorner, distance1, Vector3.forward);
            rightDownCorner = CalculateSum(side2ToRightCorner, distance2, Vector3.back);
            
            if (side2ToLeftCorner.z.Equals(side1ToLeftCorner.z))
            {
                leftUpCorner = CalculateSum(side1ToLeftCorner, distance2, Vector3.forward);
            }
            
            if (side2ToRightCorner.z.Equals(side1ToRightCorner.z))
            {
                rightUpCorner = CalculateSum(side1ToRightCorner, distance1, Vector3.back);
            }

            var sideZ = Vector3.Distance(leftUpCorner, rightUpCorner);
            var sideX = Vector3.Distance(leftUpCorner, leftDownCorner);

            for (var x = 0; x < sideX; x++)
            {
                for (var z = 0; z < sideZ; z++)
                {
                    var position = new Vector3(x + .5f, 0, z + .5f);
                    var cell = Instantiate(Prefab, position, Quaternion.identity);
                    
                    _cells.Add(position, cell);
                }
            }

            var cellsToRemove = new List<Vector3>();
            
            foreach (var cell in _cells.Where(cell => !IsInPolygon(cell.Key, Positions)))
            {
                cellsToRemove.Add(cell.Key);
                Destroy(cell.Value.gameObject);
            }

            foreach (var cell in cellsToRemove)
            {
                _cells.Remove(cell);
            }
        }

        private bool IsInPolygon(Vector3 cell, List<Vector2> polygon)
        {
            var intersects = new List<float>();
            var a = polygon.Last();
            
            foreach (var b in polygon)
            {
                if (b.x.Equals(cell.x) && b.y.Equals(cell.z)) return true;
                if (b.x.Equals(a.x) && cell.x.Equals(a.x) && cell.x >= Math.Min(a.y, b.y) && cell.z <= Math.Max(a.y, b.y)) return true;
                if (b.y.Equals(a.y) && cell.z.Equals(a.y) && cell.x >= Math.Min(a.x, b.x) && cell.x <= Math.Max(a.x, b.x)) return true;
                if ((b.y < cell.z && a.y >= cell.z) || (a.y < cell.z && b.y >= cell.z))
                {
                    var px = (int)(b.x + 1.0 * (cell.z - b.y) / (a.y - b.y) * (a.x - b.x));
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
            
            var (side1, side2) = FindSides(Positions);
            var side2ToRightCorner = side2.Last();
            var side2ToLeftCorner = side2.First();
            var side1ToRightCorner = side1.First();
            var side1ToLeftCorner = side1.Last();
            
            var distance1 = Mathf.Sqrt((float)Math.Pow(side2ToRightCorner.z - FindRightPoint().y, 2));
            Debug.DrawRay(side2ToRightCorner, new Vector3(0, 0, -distance1));
            
            var distance2 = Mathf.Sqrt((float)Math.Pow(side2ToLeftCorner.z - FindLeftPoint().y, 2));
            Debug.DrawRay(side2ToLeftCorner, new Vector3(0, 0, distance2));
            
            if (side2ToLeftCorner.z.Equals(side1ToLeftCorner.z))
            {
                Debug.DrawRay(side1ToLeftCorner, new Vector3(0, 0, distance2));
            }
            
            if (side2ToRightCorner.z.Equals(side1ToRightCorner.z))
            {
                Debug.DrawRay(side1ToRightCorner, new Vector3(0, 0, -distance1));
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