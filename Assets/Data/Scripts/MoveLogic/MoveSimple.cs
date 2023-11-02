using GridSystem;
using System.Collections.Generic;

namespace Corners
{
    //фишка не может перепрыгивать, а делает только один шаг в любом направлении
    public class MoveSimple : AbstractMove
    {
        public override List<GridCell> CheckVariations(GridCell curretnCell, Grid grid, GridInfo<Chip> gridInfo)
        {
            oponentsChipsInRange.Clear();
            List<GridCell> availableCells = new List<GridCell>();

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
                xInd = curretnCell.Coordinates.x + xOffset;
                yInd = curretnCell.Coordinates.z + yOffset;
                cell = grid.GetCellFromIndicies(xInd, yInd);
                var cellInfo = gridInfo.GetCellInfo(cell);
                var currentCellInfo = gridInfo.GetCellInfo(curretnCell);
                AddCellToList(cellInfo, availableCells, currentCellInfo);

            }
        }

        public override bool IsMoveComplete()
        {
            return true;
        }
    }
}

