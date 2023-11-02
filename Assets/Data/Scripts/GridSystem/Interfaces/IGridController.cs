using System;
using UnityEngine;

namespace GridSystem
{
    public interface IGridController
    {
        Action<GridCell> OnCellEnter { get; set; }
        Action<GridCell> OnCellExit { get; set; }
        Action<GridCell> OnCellOver { get; set; }
        Grid Grid { get; set; }

        void CheckPosition(Vector3 positionOnGrid, Action<GridCell> onHit);
    }
}
