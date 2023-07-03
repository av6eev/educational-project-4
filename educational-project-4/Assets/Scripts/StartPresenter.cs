using BuildDialog;
using Grid;
using GridBuildingsStatistic;
using UnityEngine;
using Utilities;

public class StartPresenter : MonoBehaviour
{
    public GameView View;
    
    private readonly GameManager _manager = new();
    private readonly PresentersEngine _presenters = new();
    private readonly SystemsEngine _systems = new();
    
    private void Start()
    {
        _manager.GameView = View;
        _manager.Descriptions = new GameDescriptions(View.DescriptionsCollection);
        _manager.GridModel = new GridModel();
        _manager.BuildDialogModel = new BuildDialogModel(_manager.Descriptions.Builds);
        _manager.StatisticModel = new GridBuildingsStatisticModel(_manager.Descriptions.BuildsCategory);
        
        _systems.Add(SystemTypes.GridPlacement, new GridPlacementSystem(_manager));
        
        _presenters.Add(new GridBuildingsStatisticPresenter(_manager, _manager.StatisticModel));
        _presenters.Add(new BuildDialogPresenter(_manager, _manager.BuildDialogModel, View.BuildDialogView));
        _presenters.Add(new GridPresenter(_manager, _manager.GridModel, View.GridView));
        _presenters.Activate();
    }

    private void Update()
    {
        _systems.Update(Time.deltaTime);
    }
}
