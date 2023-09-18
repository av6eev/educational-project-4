using System.Linq;
using Building;
using UnityEngine;
using Utilities;

namespace Requirements.Capitol.FifthLevel
{
    public class CapitolFifthLevelRequirementPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly CapitolFifthLevelRequirement _model;
        private BuildingModel _mainBuilding;

        public CapitolFifthLevelRequirementPresenter(GameManager manager, CapitolFifthLevelRequirement model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            _mainBuilding = _manager.ShipModel.GetActiveFloor.RegisteredBuildings.Values.First(item => item.Specification.Category == "Главное");
            _mainBuilding.OnLevelUpdated -= Check;
        }

        public void Activate()
        {
            _model.Check(_manager);
            
            _mainBuilding = _manager.ShipModel.GetActiveFloor.RegisteredBuildings.Values.First(item => item.Specification.Category == "Главное");
            _mainBuilding.OnLevelUpdated += Check;
        }
        
        private void Check()
        {
            if (_model.Check(_manager))
            {
                Debug.Log("true from level presenter");                
            }
        }
    }
}