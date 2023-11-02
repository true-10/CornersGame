using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public sealed class GridSector
    {
        public int Index { get; private set; }
        public Vector3Int GridSize { get; private set; }
        private List<GridCell> Cells { get; set; }

        private Bounds bounds;

        public GridSector(List<GridCell> cells, int index, Vector3Int gridSize)
        {
            this.Cells = cells;
            this.Index = index;
            this.GridSize = gridSize;
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            Vector2 minBorder = Vector2.zero;
            Vector2 maxBorder = Vector2.zero;
            Vector3 center = Vector3.zero;
            Vector3 size = Vector3.one;
            if (Cells.Count == 0)
            {
                bounds = new Bounds(center, size);
                return;
            }
            var cellSize = Cells[0].Size;
            foreach (var cell in Cells)
            {
                center += cell.Position;
                Vector3 point = cell.Position + cell.Size / 2f;
            }
            center /= Cells.Count;
            size.x = GridSize.x * cellSize.x;
            size.y = GridSize.y * cellSize.y;
            size.z = GridSize.z * cellSize.z;
            bounds = new Bounds(center, size);
        }

        public GridCell GetCellByPosition(Vector3 point)
        {
            if (bounds.Contains(point))
            {
                foreach (var cell in Cells)
                {
                    if (cell.CheckPoint(point))
                    {
                        return cell;
                    }
                }
            }
            return null;
        }
    }
}
