using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utilities.Helpers
{
    public static class CosmicShipFloorCalculationsHelper
    {
        public static List<Vector3Int> DefineCellsInsideGivenArea(List<Vector3> positions, bool isDebugging, bool joinLastAndFirstVertex = false, bool drawLinesForRectangle = false, bool generateTestTemplate = false)
        {
            if (positions.Count == 0) return null;

            if (!EditorApplication.isPlaying)
            {
                Gizmos.color = Color.blue;

                for (var i = 1; i < positions.Count; i++)
                {
                    Gizmos.DrawLine(new Vector3(positions[i - 1].x, positions[i - 1].y, positions[i - 1].z), new Vector3(positions[i].x, positions[i].y, positions[i].z));
                }

                if (joinLastAndFirstVertex)
                {
                    Gizmos.DrawLine(new Vector3(positions[0].x, positions[0].y, positions[0].z), new Vector3(positions[^1].x, positions[^1].y, positions[^1].z));
                }
            }
            
            var returnCells = new List<Vector3Int>();
            
            var leftUpCorner = Vector3.zero;
            var rightUpCorner = Vector3.zero;

            var (zMaxSide, zMinSide) = FindSides(positions);
            
            var zMinLeftPoint = zMinSide.Last();
            var zMinRightPoint = zMinSide.First();
            var zMaxLeftPoint = zMaxSide.First();
            var zMaxRightPoint = zMaxSide.Last();

            if (!EditorApplication.isPlaying)
            {
                Gizmos.DrawSphere(zMinLeftPoint, .5f);
                Gizmos.DrawSphere(zMinRightPoint, .5f);
                Gizmos.DrawSphere(zMaxLeftPoint, .5f);
                Gizmos.DrawSphere(zMaxRightPoint, .5f);
            }

            var distanceToMinRightPoint = Mathf.Sqrt((float)Math.Pow(zMinRightPoint.x - FindRightPoint(positions).x, 2));
            var distanceToMinLeftPoint = Mathf.Sqrt((float)Math.Pow(zMinLeftPoint.x - FindLeftPoint(positions).x, 2));

            var leftDownCorner = CalculateSum(zMinLeftPoint, distanceToMinLeftPoint, Vector3.left);
            var rightDownCorner = CalculateSum(zMaxRightPoint, distanceToMinRightPoint, Vector3.right);
            
            if (drawLinesForRectangle || !isDebugging)
            {
                if (!EditorApplication.isPlaying)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawRay(zMinLeftPoint, new Vector3(-distanceToMinLeftPoint, 0, 0));
                    Gizmos.DrawRay(zMinRightPoint, new Vector3(distanceToMinRightPoint, 0, 0));
                }

                if (zMinLeftPoint.z.Equals(zMaxLeftPoint.z))
                {
                    leftUpCorner = CalculateSum(zMaxLeftPoint, distanceToMinLeftPoint, Vector3.left);
                    if(!EditorApplication.isPlaying) Gizmos.DrawRay(zMaxLeftPoint, new Vector3(-distanceToMinLeftPoint, 0, 0));
                }
                else
                {
                    var distanceToMaxLeftPoint = Mathf.Sqrt((float)Math.Pow(zMaxLeftPoint.x - FindLeftPoint(positions).x, 2));
                    
                    leftUpCorner = CalculateSum(zMaxLeftPoint, distanceToMaxLeftPoint, Vector3.left);

                    if (!EditorApplication.isPlaying)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawRay(zMaxLeftPoint, new Vector3(-distanceToMaxLeftPoint, 0, 0));
                    }
                }
                
                if (zMinRightPoint.z.Equals(zMaxRightPoint.z))
                {
                    rightUpCorner = CalculateSum(zMaxRightPoint, distanceToMinRightPoint, Vector3.right);
                    if(!EditorApplication.isPlaying) Gizmos.DrawRay(zMaxRightPoint, new Vector3(distanceToMinRightPoint, 0, 0));
                }
                else
                {
                    var distanceToMaxRightPoint = Mathf.Sqrt((float)Math.Pow(zMaxRightPoint.x - FindRightPoint(positions).x, 2));
                    
                    rightUpCorner = CalculateSum(zMaxRightPoint, distanceToMaxRightPoint, Vector3.right);

                    if (!EditorApplication.isPlaying)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawRay(zMaxRightPoint, new Vector3(distanceToMaxRightPoint, 0, 0));    
                    }
                }
            }

            if (generateTestTemplate || !isDebugging)
            {
                var sideX = Vector3.Distance(leftDownCorner, rightDownCorner);
                var sideZ = Vector3.Distance(leftUpCorner, leftDownCorner);
                
                for (var x = leftDownCorner.x; x < sideX + leftDownCorner.x + 1; x++)
                {
                    for (var z = leftDownCorner.z + 1; z < sideZ + leftDownCorner.z + 1; z++)
                    {
                        var position = new Vector3Int((int)x, (int)positions.First().y, (int)z);

                        if (!EditorApplication.isPlaying)
                        {
                            Gizmos.color = IsInPolygon(position, positions) ? Color.white : Color.black;
                            Gizmos.DrawWireCube(new Vector3(position.x + .5f, position.y, position.z - .5f), Vector3.one);   
                        }
                        
                        returnCells.Add(position);
                    }
                }
            }

            var cellsToRemove = returnCells.Where(cell => !IsInPolygon(cell, positions)).ToList();
            
            foreach (var cell in cellsToRemove)
            {
                returnCells.Remove(cell);
            }

            return returnCells;
        }
         
        private static bool IsInPolygon(Vector3 cell, List<Vector3> positions)
        {
             var intersects = new List<float>();
             var lastVertex = positions.Last();
            
             foreach (var vertex in positions)
             {
                 if ((vertex.z < cell.z && lastVertex.z >= cell.z) || (lastVertex.z < cell.z && vertex.z >= cell.z))
                 {
                     var px = (int)(vertex.x + 1f * (cell.z - vertex.z) / (lastVertex.z - vertex.z) * (lastVertex.x - vertex.x));
                     intersects.Add(px);
                 }
            
                 lastVertex = vertex;
             }
            
             intersects.Sort();
            
             var count = intersects.Count(x => x <= cell.x);
             return intersects.IndexOf(cell.x) % 2 == 0 || count % 2 == 1;
        }

        private static Vector3 FindLeftPoint(List<Vector3> positions)
        {
            var minX = positions[0].x;
            var minZ = positions[0].z;
            float y = 0;

            minX = positions.Select(position => position.x).Prepend(minX).Min();

            foreach (var position in positions.Where(position => position.x.Equals(minX) && position.z < minZ))
            {
                minZ = position.z;
                y = position.y;
            }

            if (!EditorApplication.isPlaying)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(new Vector3(minX, y, minZ), .5f);    
            }
            
            return new Vector3(minX, y, minZ);
        }

        private static Vector3 FindRightPoint(List<Vector3> positions)
        {
            var maxX = positions[0].x;
            var maxZ = positions[0].z;
            float y = 0;

            maxX = positions.Select(position => position.x).Prepend(maxX).Max();

            foreach (var position in positions.Where(position => position.x.Equals(maxX) && position.z > maxZ))
            {
                maxZ = position.z;
                y = position.y;
            }

            if (!EditorApplication.isPlaying)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(new Vector3(maxX, y, maxZ), .5f);
            }
            
            return new Vector3(maxX, y, maxZ);
        }

        private static (List<Vector3>, List<Vector3>) FindSides(List<Vector3> positions)
        {
            var minZ = positions[0].x;
            var maxZ = positions[0].x;

            var zMaxSide = new List<Vector3>();
            var zMinSide = new List<Vector3>();

            for (var i = 0; i < positions.Count; i++)
            {
                if (positions[i].z < minZ)
                {
                    minZ = positions[i].z;
                }

                if (positions[i].z > maxZ)
                {
                    maxZ = positions[i].z;
                }
            }

            foreach (var position in positions)
            {
                if (position.z.Equals(maxZ))
                {
                    zMaxSide.Add(new Vector3(position.x, position.y, position.z));
                }

                if (position.z.Equals(minZ))
                {
                    zMinSide.Add(new Vector3(position.x, position.y, position.z));
                }
            }

            return (zMaxSide, zMinSide);
        }

        private static Vector3 CalculateSum(Vector3 point, float distance, Vector3 direction)
        {
            var result = Vector3.zero;

            result.z = point.z;

            if (direction == Vector3.right)
            {
                result.x = point.x + distance;
            }

            if (direction == Vector3.left)
            {
                result.x = point.x - distance;
            }

            return result;
        }
    }
}