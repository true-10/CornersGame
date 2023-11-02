using Corners;
using Corners.UI;

public class StateStartMenu : IState
{
    public int Type => (int)StateType.StartMenu;

    private UIController uiController;

    public StateStartMenu(UIController uiController)
    {
        this.uiController = uiController;
    }

    public void OnEnter()
    {
        uiController.ShowStartGameMenu();
    }

    public void OnExit()
    {
        uiController.ShowGameMenu();
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }
}
