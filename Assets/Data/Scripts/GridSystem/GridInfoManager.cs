using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace GridSystem
{
    public sealed class GridInfoManager<T>
    {
        public GridInfo<T> GridInfo { get; private set; }

        public void Init(ReadOnlyCollection<GridCell> gridCells)
        {
            GridInfo = new()
            {
                AvailableCells = new()
            };

            foreach (var gridCell in gridCells)
            {
                CellInfo<T> cellInfo = new()
                {
                    GridCell = gridCell,
                    Object = default,

                };
                UpdateCellInfo(gridCell, cellInfo);
            }
        }


        public void UpdateCellInfo(GridCell gridCell, CellInfo<T> cellInfo)
        {
            if (GridInfo == null)
            {
                GridInfo = new();
            }
            GridInfo.UpdateCellInfo(gridCell, cellInfo);
        }
       
        public CellInfo<T> GetCellInfoByIndex(int index)   =>  GridInfo?.GetCellInfo(index);        
    }
}

