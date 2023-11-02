using GridSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Corners
{
    //фишка перепрыгивает по вертикали и горизонтали.
    //ПОСЛЕ ПРЫЖКА МЫ МОЖЕМ ТОЛЬКО ПЕРЕПРЫГИВАТЬ!!!
    public class MoveHV : MoveSimple
    {
        //надо определить, прыгнули мы или просто сходили
        public override List<GridCell> CheckVariations(GridCell curretnCell, GridSystem.Grid grid, GridInfo<Chip> gridInfo)
        {
            List<GridCell> availableCells = base.CheckVariations(curretnCell, grid, gridInfo);
            //если продолжительный ход, то убираем соседние клетки из доступных ходов
            if (isLongMoveActive) availableCells.Clear();

            int availableCellCount = availableCells.Count;
            isJumpingMovesAvailable = false;
            for (int i = 0; i < oponentsChipsInRange.Count; i++)
            {
                Chip ch = oponentsChipsInRange[i];
                Vector3Int ourCellInds = curretnCell.Coordinates;
                Vector3Int chInds = ch.Holder.Coordinates;

                //определяем положение ch по сетке относительно нашей фишки
                if (ourCellInds.x == chInds.x || ourCellInds.y == chInds.y)
                {
                    //мы в одном ряду или столбце
                    int xOffset = ourCellInds.x - chInds.x;
                    int yOffset = ourCellInds.y - chInds.y;
                    xInd = chInds.x - xOffset;
                    yInd = chInds.y - yOffset;
                    cell = grid.GetCellFromIndicies(xInd, yInd);
                    var cellInfo = gridInfo.GetCellInfo(cell);
                    var currentCellInfo = gridInfo.GetCellInfo(curretnCell);
                    AddCellToList(cellInfo, availableCells, currentCellInfo);
                }
            }
            //если стало больше доступных ходов
            if (availableCells.Count > availableCellCount || availableCells.Count == 0)
                isJumpingMovesAvailable = true;

            isLongMoveActive = true;
            return availableCells;
        }

        public override bool IsMoveComplete()
        {
            return !isJumpingMovesAvailable;
        }
    }
}
