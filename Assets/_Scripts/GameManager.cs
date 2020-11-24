using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region fields
    [SerializeField] private Player _player1;
    [SerializeField] private Player _player2;
    [SerializeField] private Grid _grid;
    [SerializeField] private ChipLoader _chipLoader;

    [SerializeField] private UI_StartMenu _startMenu;
    [SerializeField] private UI_EndGameScreen _endGameMenu;
    [SerializeField] private UI_Game _gameUI;    

    private StateMachine _stateMachine;
    private CheckEndGame _checkEndGameProcessor;
    private Move _moveProcessor;
    
    #endregion

    void Start()
    {
        InitMoveProcessor();
        _checkEndGameProcessor = new CheckEndGame(_player1, _player2);
        InitStateMachine();
        FillFieldWithChips();
        _player1.Init(_grid, _moveProcessor);
        _player2.Init(_grid, _moveProcessor);
        _checkEndGameProcessor.Init();
    }

    public void Init()
    {
         InitMoveProcessor();
        _player1.Init(_grid, _moveProcessor);
        _player2.Init(_grid, _moveProcessor);
    }

    void Update()
    {
        _stateMachine.OnUpdate();
    }

    void InitStateMachine()
    {
        _stateMachine = new StateMachine();

        var menu = new StartMenuState(_startMenu, _gameUI, this);
        var p1 = new PlayerMoveState(_player1);
        var p2 = new PlayerMoveState(_player2);
        var checkEndGame = new CheckEndGameState(_checkEndGameProcessor);
        var endGame = new EndGameState(_endGameMenu, _gameUI, _checkEndGameProcessor, _grid, _player1, _player2);

        _stateMachine.AddTransition(menu, p1, TimeToPlay() );
        _stateMachine.AddTransition(p1, p2, SwitchToPlayer2());
        _stateMachine.AddTransition(p2, checkEndGame, SwitchToCheckEnd());
        _stateMachine.AddTransition(checkEndGame, p1, SwitchToPlayer1());
        _stateMachine.AddTransition(checkEndGame, endGame, EndGame());
        _stateMachine.AddTransition(endGame, p1, Restart());
        _stateMachine.AddTransition(endGame, menu, ToStartMenu());

        _stateMachine.AddAnyTransition(menu, ExitGame());

        _stateMachine.SetState(menu);
        //условия перехода
        Func<bool> TimeToPlay() => () => _startMenu._startGame;//начинаем игру из стартового меню
        Func<bool> SwitchToPlayer2() => () => _player1._endMove;
        Func<bool> SwitchToCheckEnd() => () => _player2._endMove;
        Func<bool> SwitchToPlayer1() => () => _checkEndGameProcessor.ContinueGame();
        Func<bool> EndGame() => () => !_checkEndGameProcessor.ContinueGame(); ; //ничья или ктото победил
        Func<bool> Restart() => () => _endGameMenu._restartGame;//рестарт игры из экрана конца игры
        Func<bool> ToStartMenu() => () => _endGameMenu._mainMenu;//переходим в стартовое меню из экрана конца игры
        Func<bool> ExitGame() => () => false;//выход в стартовое меню из любого состояния
    }

    public void InitMoveProcessor()
    {
        switch(_startMenu.GetMoveType() )
        {
            case MoveType.MT_Simple:    _moveProcessor = new SimpleMove();  break;
            case MoveType.MT_HV:        _moveProcessor = new HVMove();      break;
            case MoveType.MT_Checkers:  _moveProcessor = new ChekersMove(); break;
            default: _moveProcessor = new SimpleMove(); break;
        }
    }

    void FillFieldWithChips()
    {
        _chipLoader.Init();

        List<Chip> whiteChips = _chipLoader.GetListOfWhiteChips(9);
        _player1.SetChips(whiteChips);
        _grid.SetWhiteChipsOnField(whiteChips, 3);

        List<Chip> blackChips = _chipLoader.GetListOfBlackChips(9);
        _player2.SetChips(blackChips);
        _grid.SetBlackChipsOnField(blackChips, 3);
    }
}
