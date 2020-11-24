using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//фишка не может перепрыгивать, а делает только один шаг в любом направлении
public class SimpleMove : Move
{
    public override List<Cell> CheckVariations(Cell curretnCell, Grid grid)
    {
        _oponentsChips.Clear();
        List<Cell> availableCells = new List<Cell>();

        AddCell(1, 0);//право середина
        AddCell(-1, 0);//лево середина
        AddCell(0, 1);//середина верх
        AddCell(0, -1);//середина низ
        AddCell(1, 1);//право верх
        AddCell(-1, 1);//лево верх
        AddCell(1, -1);//право низ
        AddCell(-1, -1);//лево низ

        return availableCells;

        void AddCell(int xOffset, int yOffset)
        {
            xInd = curretnCell.inds.x + xOffset;
            yInd = curretnCell.inds.y + yOffset;
            cell = grid.GetCellFromIndicies(xInd, yInd);
            AddCellToList(cell, availableCells, curretnCell);
        }
    }

    public override bool IsMoveComplete()
    {
        return true;
    }
}

