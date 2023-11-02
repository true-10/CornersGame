using System.Collections.Generic;
using System;
using GridSystem;

namespace Corners
{

    public abstract class AbstractPlayer
    {
        protected List<Chip> chips; //фишки принадлежащие этому игроку
        protected Chip activeChip; //выделенная фишка
        protected GridSystem.Grid grid;
        protected int moveCount = 0; //кол-во ходов
        protected int movePerTurn = 0; //кол-во движений фишки за ход

        public PlayerColor Color { get; set; }
        public bool IsMoveEnded { get; protected set; }
        public Action<AbstractPlayer, int> OnEndMove;

        protected AbstractMove moveProcessor;
        protected GridInfoManager<Chip> gridInfoManager;
        protected IGridInput input;
        protected IGridController gridController;
        public abstract void UpdateLogic();

        public void SetChips(List<Chip> chipList)
        {
            chips = chipList;

            foreach (Chip ch in chips)
            {
                ch.OnChipClick += SetActiveChip;
            }
        }

        private void SetActiveChip(Chip chip)
        {
            if (activeChip != null)
            {
                //если какая то фишка была выделена до этого, то скейлим обратно
                activeChip.DefaultScale();
            }
            activeChip = chip;

            moveProcessor.ResetLongMove();
            var currentCell = activeChip.Holder;

            gridInfoManager.GridInfo.AvailableCells = moveProcessor.CheckVariations(currentCell, grid, gridInfoManager.GridInfo);
            activeChip.SelectedScale();
        }

        public virtual void Init(IGridController gridController, IGridInput gridInput, AbstractMove moveProcessor, GridInfoManager<Chip> gridInfoManager)
        {
            moveCount = 0;
            activeChip = null;
            this.gridController = gridController;
            this.grid = gridController.Grid;
            this.moveProcessor = moveProcessor;
            this.gridInfoManager = gridInfoManager;
        }

        public void ResetParams()
        {
            activeChip = null;
            IsMoveEnded = false;
            movePerTurn = 0;
            EnableChips(false);
        }

        public List<Chip> GetChips() => chips;

        public int GetMoveCount() => moveCount;

        public void ResetMoveCount() => moveCount = 0;

        public void RemoveChips()
    {
        foreach (Chip ch in chips)
        {
            ch.OnChipClick -= SetActiveChip;
        }
        chips.Clear();
    }


        public void EndMove()
        {
            gridInfoManager.GridInfo.AvailableCells.Clear();
            activeChip.DefaultScale();
            activeChip = null;
            moveCount++;
            IsMoveEnded = true;
            moveProcessor.ResetLongMove();
            OnEndMove?.Invoke(this, moveCount);
        }

        public void MoveChip(GridCell targetCell)
        {
            EnableChips(false);//выключаем другие фишки, что бы нельзя было их выбрать
            movePerTurn++;

            var currentCell = activeChip.Holder;
           // var prevCell = activeChip.PrevHolder;
            var currentCellInfo = gridInfoManager.GetCellInfoByIndex(currentCell.Index);
            //var prevCellInfo = gridInfoManager.GetCellInfoByIndex(prevCell.Index);
            var targetCellInfo = gridInfoManager.GetCellInfoByIndex(targetCell.Index);

            if (currentCell != null)
            {
                /*if (prevCellInfo != null)
                {
                    prevCellInfo.Object = currentCellInfo.Object;//activeChip.Holder.isEmpty = false;
                    gridInfoManager.UpdateCellInfo(prevCell, prevCellInfo);
                }*/
                currentCellInfo.Object = null;//activeChip.Holder._chip = null;
                gridInfoManager.UpdateCellInfo(currentCell, currentCellInfo);
            }
            activeChip.PrevHolder = activeChip.Holder;

            activeChip.Move(targetCell.Position);
             activeChip.Holder = targetCell;
            if (targetCellInfo != null) 
            {
                targetCellInfo.Object = activeChip;
                gridInfoManager.UpdateCellInfo(targetCell, targetCellInfo);
            }
            // activeChip.Holder.chip = activeChip;
            //grid.HideSelectedQuad();

            if (moveProcessor.IsMoveComplete())
            {
                EndMove();
                return;
            }
            else
            {
                //  grid.CheckMoveVariations(moveProcessor, activeChip._holder);
            }
            if (!activeChip.WasItJump() || gridInfoManager.GridInfo.IsNoMoreMoves())
            {
                EndMove();
                return;
            }
        }

        public void EnableChips(bool available)
        {
            foreach (Chip ch in chips)
            {
                ch.IsAvalable = available;
            }
        }

    }
}


/* protected void CellDebug(GridCell cell)
 {
     UnityEngine.Debug.Log($"--------------------");
     UnityEngine.Debug.Log($"GridCell: {cell} index = {cell?.Index}");
     UnityEngine.Debug.Log($"Position = {cell?.Position} Coordinates = {cell?.Coordinates}");
 }
 protected void ChipDebug(Chip chip)
 {
     UnityEngine.Debug.Log($"[ChipDebug]Chip---------");
     UnityEngine.Debug.Log($"Chip: {chip} color = {chip?.Color} IsAvalable = {chip?.IsAvalable}");
     UnityEngine.Debug.Log($"[ChipDebug]Holder---------");
     CellDebug(chip?.Holder);
     UnityEngine.Debug.Log($"[ChipDebug]PrevHolder---------");
     CellDebug(chip?.PrevHolder);
 }*/