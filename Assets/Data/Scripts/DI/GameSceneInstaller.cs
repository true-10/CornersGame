using Corners;
using Corners.UI;
using GridSystem;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameSettingsSO gameSettings;
    [SerializeField]
    private UIController uiController;

    public override void InstallBindings()
    {
        Container.Bind<GameSettingsSO>().To<GameSettingsSO>().FromScriptableObject(gameSettings).AsSingle().NonLazy();
        Container.Bind<CheckEndGameProcessor>().AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<PlayerManager>().AsSingle();
        Container.Bind<GridInfoManager<Chip>>().AsSingle();

        Container.Bind<IGridController>().FromMethod(GetGridController).AsSingle();
        Container.Bind<UIController>().FromInstance(uiController).AsSingle();

        InstallUI();
        InstallStates();

    }

    private void InstallUI()
    {
    }

    private void InstallStates()
    {
        Container.Bind<StateFabric>().AsSingle();
        Container.Bind<StateManager>().AsSingle();
        Container.Bind<StateMachine>().AsSingle();
    }

    private IGridController GetGridController()
    {

        var gridData = new GridData()
        {
            GridSize = gameSettings.GridSize,
            GridType = GridType.Square,
            Offset = gameSettings.Offset,
            Cellize = gameSettings.CellSize
            //StartPos = startPos
        };
       return new GridController(gridData, gameSettings.SectorSize);
    }
}