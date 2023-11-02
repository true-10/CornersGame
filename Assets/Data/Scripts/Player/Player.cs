using System.Collections;
using UnityEngine;
using DG.Tweening;
using GridSystem;

namespace Corners
{
    public enum PlayerColor
    {
        White,
        Black
    }

    public enum GameMode
    {
        WithPlayer,
        WithAI
    }

    public sealed class Player : AbstractPlayer
    {

        private GridCell activeCell;

        public override void Init(IGridController gridController, IGridInput gridInput, AbstractMove moveProcessor, GridInfoManager<Chip> gridInfoManager)
        {
            base.Init(gridController, gridInput, moveProcessor, gridInfoManager);

            gridInput.OnInput -= OnInputHandler;
            gridInput.OnInput += OnInputHandler;
        }

        public override void UpdateLogic()
        {
            //лкм
            if (Input.GetMouseButtonDown(0))
            {
                if (activeChip != null)
                {
                    if (activeCell == null) return;
                    MoveChip(activeCell);
                }
            }
            //пкм - завершение хода
            if (!moveProcessor.IsMoveComplete())
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (movePerTurn > 0)
                    {
                        //нельзя завершить ход, если не двинул фишку
                        EndMove();
                    }
                }
            }
        }

        private void OnInputHandler(GridCell cell)
        {
            if (cell == null)
            {
                activeCell = null;
                return;
            }
            var cellInfo = gridInfoManager.GetCellInfoByIndex(cell.Index);
            if (cellInfo.IsEmpty && gridInfoManager.GridInfo.AvailableCells.Contains(cell))
            {
                activeCell = cell;
            }
            else
            {
                activeCell = null;
            }
        }
    }
}
