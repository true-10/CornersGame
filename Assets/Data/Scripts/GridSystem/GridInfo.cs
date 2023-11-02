using System.Collections.Generic;

namespace GridSystem
{
    [System.Serializable]
    public sealed class GridInfo<T>
    {
        private Dictionary<int, CellInfo<T>> cellInfos;

        public List<GridCell> AvailableCells { get; set; }
        public bool IsNoMoreMoves() => AvailableCells.Count == 0;

        public CellInfo<T> GetCellInfo(GridCell gridCell)
        {
            if (gridCell == null)
            {
                return null;
            }
            return GetCellInfo(gridCell.Index);
        }

        public CellInfo<T> GetCellInfo(int index)
        {
            if (cellInfos == null || !cellInfos.ContainsKey(index))
            {
                return null;
            }
            return cellInfos[index];
        }

        public void UpdateCellInfo(GridCell gridCell, CellInfo<T> cellInfo) 
        {
            UpdateCellInfo(gridCell.Index, cellInfo);
        }

        public void UpdateCellInfo(int index, CellInfo<T> cellInfo)
        {
            if (cellInfos == null)
            {
                cellInfos = new();
            }
            if (cellInfos.ContainsKey(index))
            {
                cellInfos[index] = cellInfo;
                return;
            }

            cellInfos.Add(index, cellInfo);
        }

    }
}

