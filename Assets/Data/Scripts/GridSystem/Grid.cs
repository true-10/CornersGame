using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace GridSystem
{
    public sealed class Grid: IGrid
    {
        public Vector3Int GridSize { get; private set; }

        private List<GridCell> cells;
        private Sectors sectors;

        public ReadOnlyCollection<GridCell> Cells => cells.AsReadOnly();

        public Grid(GridData gridData, List<GridCell> cells, Vector3Int sectorSize )
        {
            this.GridSize = gridData.GridSize;
            this.cells = cells;

            Sectors.SectorsData sectorsData = new()
            {
                GridCells = cells,
                GridSize = GridSize,
                SectorSize = sectorSize
            };
            sectors = new(sectorsData);
        }

        public GridCell GetCellByPosition(Vector3 point)
        {
            return sectors.GetCellByPosition(point);
        }

        public GridCell GetCellFromIndicies(int xInd, int yInd)
        {
            return Cells.FirstOrDefault(cell => cell.Coordinates.x == xInd && cell.Coordinates.z == yInd);
        }
    }

    public enum GridType
    {
        Square = 0, //квадратные ячейки
        Hex,    //шестиугольник
    }

    public class GridData
    {
        public GridType GridType;
        public Vector3Int GridSize;
        public Vector3 Cellize;
        public Vector3 Offset;
    }


}

