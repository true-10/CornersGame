using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : IState
{
    #region fields

    private UI_EndGameScreen _endGameMenu;
    private UI_Game _gameUI;
    private CheckEndGame _checkEndGameProcessor;

    Grid _grid;
    Player _whitePlayer;
    Player _blackPlayer;

    #endregion

    public EndGameState(UI_EndGameScreen endGameMenu, UI_Game gameUI, CheckEndGame checkEndGameProcessor,
        Grid grid, Player whitePlayer, Player blackPlayer)
    {
        _endGameMenu = endGameMenu;
        _gameUI = gameUI;
        _checkEndGameProcessor = checkEndGameProcessor;

        _grid = grid;
        _whitePlayer = whitePlayer;
        _blackPlayer = blackPlayer;
    }

    public void OnEnter()
    {
        _endGameMenu.gameObject.SetActive(true);
        _gameUI.gameObject.SetActive(false);
        _endGameMenu.UpdateText(_checkEndGameProcessor.GetWinner());
    }

    public void OnExit()
    {
        _endGameMenu.gameObject.SetActive(false);
        if (_endGameMenu._restartGame)
        {
            _gameUI.gameObject.SetActive(true);

        }
        _endGameMenu.ResetMenu();
        ResetGameParams();
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }

    void ResetGameParams()
    {
        _whitePlayer.ResetMoveCount();
        _whitePlayer.ResetParams();
        _blackPlayer.ResetMoveCount();
        _blackPlayer.ResetParams();
        _grid.ResetCells();
        _grid.SetWhiteChipsOnField(_whitePlayer.GetChips(), 3);
        _grid.SetBlackChipsOnField(_blackPlayer.GetChips(), 3);
    }
}
