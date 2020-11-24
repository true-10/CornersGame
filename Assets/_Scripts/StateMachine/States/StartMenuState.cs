using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuState : IState
{
    #region fields

    private UI_StartMenu _startMenu;    
    private UI_Game _gameUI;
    private GameManager _gm;
    #endregion

    public StartMenuState(UI_StartMenu startMenu, UI_Game gameUI, GameManager gm)
    {
        _startMenu = startMenu;
        _gameUI = gameUI;
        _gm = gm;
    }
    public void OnEnter()
    {
        _startMenu.gameObject.SetActive(true);
        _gameUI.gameObject.SetActive(false);
    }

    public void OnExit()
    {
        _startMenu.gameObject.SetActive(false);
        _gameUI.gameObject.SetActive(true);
        _startMenu.ResetMenu();
        _gm.Init();
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }
}
