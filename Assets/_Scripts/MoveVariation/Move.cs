using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    MT_Simple = 0,
    MT_HV,
    MT_Checkers

}

public abstract class Move
{
    protected bool _jumpingMovesAvailable;
    protected bool _longMove; //когда за раз несколько раз фишку двигаешь
    protected int xInd = 0;
    protected int yInd = 0;

    protected Cell cell = null;
    protected List<Chip> _oponentsChips;//фишки опонента доступные для перепрыгивания

    public abstract List<Cell> CheckVariations(Cell curretnCell, Grid grid);
    public abstract bool IsMoveComplete();

    public Move()
    {
        _oponentsChips = new List<Chip>();
        _jumpingMovesAvailable = false;
        _longMove = false;
    }

    protected void AddCellToList(Cell c, List<Cell> list, Cell current)
    {
        if (c != null)
        {
            if (c._isEmpty)
            {
                list.Add(c);
            }
            else
            {
                if (c._chip._color != current._chip._color)
                {
                    _oponentsChips.Add(c._chip);
                }
            }
        }
    }

    public void ResetLongMove()
    {
        _longMove = false;
    }
}

