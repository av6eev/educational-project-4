using System.Collections.Generic;
using System.Linq;
using Building;
using Core.Grid;
using Cosmic.Ship.Floor;
using UnityEngine;
using UnityEngine.UI;

namespace Cosmic.Ship
{
    public class CosmicShipView : BaseGridView
    {
        [field: SerializeField] public List<CosmicShipFloorView> LevelFloorRoots { get; private set; }
        [field: SerializeField] public Button NextFloorBtn { get; private set; }
        [field: SerializeField] public Button PreviousFloorBtn { get; private set; }

        public BuildingView InstantiateBuilding(BuildingView prefab, Vector3 gridPosition, int floorLevel)
        {
            var go = Instantiate(prefab, new Vector3(gridPosition.x, gridPosition.y + .05f, gridPosition.z), Quaternion.identity);
            
            go.transform.SetParent(LevelFloorRoots[floorLevel].transform);

            return go;
        }

        public void HandleRequiredFloors(int currentFloorIndex)
        {
            TryGetElementByIndex(currentFloorIndex, out var currentFloor);
            TryGetElementByIndex(currentFloorIndex - 1, out var previousFloor);
            TryGetElementByIndex(currentFloorIndex + 1, out var nextFloor);
            
            for (var i = 0; i < LevelFloorRoots.Count; i++)
            {
                if (i == currentFloorIndex && currentFloor != null)
                {
                    currentFloor.gameObject.SetActive(true);
                }
                else if (i == currentFloorIndex - 1 && previousFloor != null)
                {
                    previousFloor.gameObject.SetActive(true);
                }
                else if (i == currentFloorIndex + 1 && nextFloor != null)
                {
                    nextFloor.gameObject.SetActive(true);
                }
                else
                {
                    LevelFloorRoots[i].gameObject.SetActive(false);
                }
            }
        }

        private void TryGetElementByIndex(int index, out CosmicShipFloorView element)
        {
            var e = LevelFloorRoots.ElementAtOrDefault(index);
            
            element = e != null ? e : null;
        }
    }
}