using UnityEngine;

namespace GridSystem
{
    public interface IGrid
    {
        Vector3Int GridSize { get; }
        GridCell GetCellByPosition(Vector3 point);
        GridCell GetCellFromIndicies(int xInd, int yInd);
    }


}

