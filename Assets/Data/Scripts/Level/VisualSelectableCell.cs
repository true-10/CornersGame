using GridSystem;
using UnityEngine;

namespace Corners
{
    public sealed class VisualSelectableCell
    {
        private Transform selectable;
        private IGridController gridController;
        private GridInfoManager<Chip> gridInfoManager;

        private Vector3 offset;

        public VisualSelectableCell(Transform transform, IGridController gridController, GridInfoManager<Chip> gridInfoManager, Vector3 offset)
        {
            this.selectable = transform;
            this.gridController = gridController;
            this.gridInfoManager = gridInfoManager;
            this.offset = offset;

            gridController.OnCellOver += OnCellInput;
        }

        private void OnCellInput(GridCell cell)
        {
            if (cell == null)
            {
                return;
            }
            if (gridInfoManager.GridInfo.AvailableCells.Contains(cell))
            {
                selectable.position = cell.Position + offset;
            }
            else
            {
                selectable.position = Vector3.down * 10f;

            }          
        }
    }
}
