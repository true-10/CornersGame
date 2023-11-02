using GridSystem;
using System.Collections.Generic;

namespace Corners
{

    public enum MoveType
    {
        Simple = 0,
        HorizontalAndVertical,
        CheckersLike

    }

    public abstract class AbstractMove //AvailableMovesProcessor
    {
        protected bool isJumpingMovesAvailable;
        protected bool isLongMoveActive; //когда за раз несколько раз фишку двигаешь
        protected int xInd = 0;
        protected int yInd = 0;

        protected GridCell cell = null;
        protected List<Chip> oponentsChipsInRange = new();//фишки опонента доступные для перепрыгивания

        public abstract List<GridCell> CheckVariations(GridCell curretnCell, Grid grid, GridInfo<Chip> gridInfo);
        public abstract bool IsMoveComplete();

        public AbstractMove()
        {
            // oponentsChipsInRange = new List<Chip>();
            isJumpingMovesAvailable = false;
            isLongMoveActive = false;
        }

        protected void AddCellToList(CellInfo<Chip> targetCellInfo, List<GridCell> list, CellInfo<Chip> currentCellInfo)
        {
            if (targetCellInfo == null)
            {
                return;
            }
            if (targetCellInfo.IsEmpty)
            {
                list.Add(targetCellInfo.GridCell);
            }
            else
            {
                if (currentCellInfo.Object.TryGetComponent<Chip>(out var currentChip))
                {
                    if (targetCellInfo.Object.TryGetComponent<Chip>(out var chip))
                    {
                        if (chip.Color != currentChip.Color)
                        {
                            oponentsChipsInRange.Add(chip);
                        }
                    }
                }

            }
        }

        public void ResetLongMove()
        {
            isLongMoveActive = false;
        }
    }
}

