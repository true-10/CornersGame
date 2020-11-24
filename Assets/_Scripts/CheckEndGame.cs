using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс проверки окончания игры
public class CheckEndGame
{
    #region fields
    private Player _whitePlayer;
    private Player _blackPlayer;

    //когда фишки одного соперника полностью окажутся на базе другого, то победа
    //после 80 хода черного игра останавливается и ведем подсчет
    //у кого фишек в базе врага больше, тот и победил
    //или ничья
    private List<Cell> _whiteStartCells;
    private List<Cell> _blackStartCells;

    private List<Chip> _blackChips;
    private List<Chip> _whiteChips;

    private int _whiteChipsInBlackBase = 0;
    private int _blackChipsInWhiteBase = 0;
    #endregion

    public CheckEndGame(Player white, Player black)
    {
        _whitePlayer = white;
        _blackPlayer = black;
    }

    public void Init()
    {
        _whiteStartCells = new List<Cell>();
        _blackStartCells = new List<Cell>();

        _whiteChips = _whitePlayer.GetChips();
        _blackChips = _blackPlayer.GetChips();
        //сохраняем ячейки базы
        foreach (Chip ch in _whiteChips)
        {
            _whiteStartCells.Add(ch._holder);
        }
        foreach (Chip ch in _blackChips)
        {
            _blackStartCells.Add(ch._holder);
        }
    }

    public Player GetWinner()
    {
        Player winner = null;
        if (_whiteChipsInBlackBase > _blackChipsInWhiteBase) winner = _whitePlayer;
        if (_blackChipsInWhiteBase > _whiteChipsInBlackBase) winner = _blackPlayer;

        return winner;
    }

    public bool ContinueGame()
    {
        bool result = true;
        if (_whiteChipsInBlackBase == 9) result = false;
        if (_blackChipsInWhiteBase == 9) result = false;
        if (OutOfMoves()) result = false;
        return result;
    }

    bool OutOfMoves()
    {//больше нет ходов
        return _blackPlayer.GetMoveCount() > 79;
    }

    public void CheckChipsPosition()
    {
        _whiteChipsInBlackBase = 0;
        foreach (Chip ch in _whiteChips)
        {
            if (_blackStartCells.Contains(ch._holder)) _whiteChipsInBlackBase++;
        }
        _blackChipsInWhiteBase = 0;
        foreach (Chip ch in _blackChips)
        {
            if (_whiteStartCells.Contains(ch._holder)) _blackChipsInWhiteBase++;
        }
    }
}
