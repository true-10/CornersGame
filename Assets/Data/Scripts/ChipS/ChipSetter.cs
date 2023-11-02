using GridSystem;
using System.Collections.Generic;
using System.Linq;

namespace Corners
{
    public sealed class ChipSetter
    {
        private Grid grid;
        private GridInfoManager<Chip> gridInfoManager;

        public ChipSetter(Grid grid, GridInfoManager<Chip> gridInfoManager)
        {
            this.grid = grid;
            this.gridInfoManager = gridInfoManager;
        }

        public void SetChipsOnGrid(List<Chip> whiteChips, List<Chip> blackChips, int offset)
        {
            SetWhiteChipsOnField(whiteChips, offset);
            SetBlackChipsOnField(blackChips, offset);
        }

        public void SetWhiteChipsOnField(List<Chip> chips, int offset)
        {
            int chipIndex = 0;
            for (int x = 0; x < offset; x++)
            {
                for (int y = 0; y < offset; y++)
                {
                    Chip chip = chips[chipIndex];
                    var cell = grid.Cells.FirstOrDefault(c => c.Coordinates.x == x && c.Coordinates.z == y);
                    SetChipOnGrid(chip, cell);
                    chipIndex++;
                }
            }
        }

        public void SetBlackChipsOnField(List<Chip> chips, int offset)
        {
            int chipIndex = 0;
            int rowCount = grid.GridSize.x;
            int colCount = grid.GridSize.z;
            for (int x = rowCount - 1; x > rowCount - 1 - offset; x--)
            {
                for (int y = colCount - 1; y > colCount - 1 - offset; y--)
                {
                    Chip chip = chips[chipIndex];
                    var cell = grid.Cells.FirstOrDefault(c => c.Coordinates.x == x && c.Coordinates.z == y);

                    SetChipOnGrid(chip, cell);
                    chipIndex++;
                }
            }
        }

        private void SetChipOnGrid(Chip chip, GridCell cell)
        {
            var cellInfo = gridInfoManager.GridInfo.GetCellInfo(cell);
            chip.Holder = cell;
            chip.PrevHolder = cell;
            if (cellInfo == null)
            {
                cellInfo = new()
                {
                    GridCell = cell
                };
            }
            chip.transform.position = cell.Position;
            cellInfo.Object = chip;
        }
    }
}