using System;

namespace GridSystem
{
    public interface IGridInput
    {
        Action<GridCell> OnInput { get; set; }
        void Tick();
    }

}
