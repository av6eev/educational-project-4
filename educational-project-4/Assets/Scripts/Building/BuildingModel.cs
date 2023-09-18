using System;
using System.Collections.Generic;
using Specifications.Builds.Buildings;
using UnityEngine;

namespace Building
{
    public class BuildingModel
    {
        public event Action OnDialogClosed, OnLevelUpdated;
        
        private string _id;
        public string Id
        {
            get => _id;
            private set
            {
                if (Convert.ToInt32(value) < 0) return;
                _id = value;
            }
        }

        public List<Vector3> TakenPositions { get; private set; }
        public BuildingSpecification Specification { get; private set; }
        public int CurrentUpgradeLevel { get; private set; } = 1;
        public bool HasActiveDialog { get; set; }

        public BuildingModel(string id, BuildingSpecification specification, List<Vector3> takenPositions)
        {
            Id = id;
            Specification = specification;
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