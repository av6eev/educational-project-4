using System;
using System.Collections.Generic;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;

namespace Building
{
    public class BuildingModel
    {
        public event Action OnDialogClosed;
        
        public string Id { get; private set; }

        public readonly BuildingDescription Description;
        public List<Vector3Int> TakenPositions;

        public bool HasActiveDialog;

        public BuildingModel(string id, BuildingDescription description, List<Vector3Int> takenPositions)
        {
            Id = id;
            Description = description;
            TakenPositions = takenPositions;
        }

        public void CloseDialog()
        {
            HasActiveDialog = false;
            OnDialogClosed?.Invoke();
        }
    }
}