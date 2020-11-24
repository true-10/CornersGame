using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//фишка перепрыгивает по вертикали и горизонтали.
//ПОСЛЕ ПРЫЖКА МЫ МОЖЕМ ТОЛЬКО ПЕРЕПРЫГИВАТЬ!!!
public class HVMove : SimpleMove
{
    //надо определить, прыгнули мы или просто сходили
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
            if (ourCellInds.x == chInds.x || ourCellInds.y == chInds.y)
            {//мы в одном ряду или столбце
                int xOffset = ourCellInds.x - chInds.x;
                int yOffset = ourCellInds.y - chInds.y;
                xInd = chInds.x - xOffset;
                yInd = chInds.y - yOffset;
                cell = grid.GetCellFromIndicies(xInd, yInd);
                AddCellToList(cell, availableCells, curretnCell);
            }
        }
        //если стало больше доступных ходов
        if (availableCells.Count > availableCellCount || availableCells.Count == 0)
            _jumpingMovesAvailable = true;

        _longMove = true;
        return availableCells;
    }

    public override bool IsMoveComplete()
    {
        return !_jumpingMovesAvailable;
    }
}
