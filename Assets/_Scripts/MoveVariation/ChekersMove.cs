using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//фишка может перепрыгивать через другую как в шашках, по диагонали. 
public class ChekersMove : SimpleMove
{
    public override List<Cell> CheckVariations(Cell curretnCell, Grid grid)
    {
        List<Cell> availableCells = base.CheckVariations(curretnCell, grid);
        //если продолжительный ход, то убираем соседние клетки из доступных ходов
        if (_longMove) availableCells.Clear();

        int availableCellCount = availableCells.Count;
        _jumpingMovesAvailable = false;
        for (int i = 0; i < _oponentsChips.Count; i++)
        {
            Chip ch = _oponentsChips[i];
            Vector2Int ourCellInds = curretnCell.inds;
            Vector2Int chInds = ch._holder.inds;
            //определяем положение ch по сетке относительно нашей фишки
            if (ourCellInds.x != chInds.x && ourCellInds.y != chInds.y)
            {
                //мы не в одном ряду и не в одном столбце
                int xOffset = ourCellInds.x - chInds.x;
                int yOffset = ourCellInds.y - chInds.y;
                xInd = chInds.x - xOffset;
                yInd = chInds.y - yOffset;
                cell = grid.GetCellFromIndicies(xInd, yInd);
                AddCellToList(cell, availableCells, curretnCell);
            }
        }
        if (availableCells.Count > availableCellCount)
            _jumpingMovesAvailable = true;
        else
        {
            _jumpingMovesAvailable = availableCells.Count == 0;
        }
        _longMove = true;
        return availableCells;
    }

    public override bool IsMoveComplete()
    {
        return !_jumpingMovesAvailable;
    }
}
