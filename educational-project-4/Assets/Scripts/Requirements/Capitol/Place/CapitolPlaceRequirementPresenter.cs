using Requirements.Capitol.FifthLevel;
using Utilities;

namespace Requirements.Capitol.Place
{
    public class CapitolPlaceRequirementPresenter : IPresenter
    {
        private readonly GameManager _manager;
        private readonly CapitolPlaceRequirement _model;

        private readonly PresentersEngine _presenters = new();
        
        public CapitolPlaceRequirementPresenter(GameManager manager, CapitolPlaceRequirement model)
        {
            _manager = manager;
            _model = model;
        }
        
        public void Deactivate()
        {
            _manager.StatisticModel.OnBuildingRegistered -= Check;
        }

        public void Activate()
        {
            _manager.StatisticModel.OnBuildingRegistered += Check;
        }
        
        private void Check(string buildingId)
        {
            if (!_model.Check(_manager)) return;
            
            foreach (var requirement in _manager.Specifications.Requirements)
            {
                switch (requirement.Value)
                {
                    case CapitolFifthLevelRequirement model:
                        var presenter = new CapitolFifthLevelRequirementPresenter(_manager, model);
                        _presenters.Add(presenter);
                        break;
                }
            }
                        
            _presenters.Activate();
        }
    }
}