using System;
using System.Collections.Generic;
using Descriptions.Builds.BuildsCategory.Buildings;
using UnityEngine;

namespace Building
{
    public class BuildingModel
    {
        public event Action OnDialogClosed, OnLevelUpdated;
        
        public string Id { get; private set; }
        public List<Vector3Int> TakenPositions;
        public int CurrentUpgradeLevel = 1;
        
        public readonly BuildingDescription Description;

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

        public void UpdateLevelUpgrade()
        {
            CurrentUpgradeLevel += 1;
            OnLevelUpdated?.Invoke();
        }
    }
}