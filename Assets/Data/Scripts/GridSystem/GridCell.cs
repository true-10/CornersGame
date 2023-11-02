using UnityEngine;

namespace GridSystem
{
    [System.Serializable]
    public sealed class GridCell
    {
        public int Index { get => index; }
        public Vector3 Size { get => size; }
        public Vector3 Position { get => position; }
        public Vector3Int Coordinates { get => coordinates; }


        private int index;
        private Vector3 position;//local
        private Vector3Int coordinates;//on grid position
        private Vector3 size;

        private Bounds bounds;

        public GridCell(int index, Vector3 pos, Vector3 size, Vector3Int coords)
        {
            this.index = index;
            this.position = pos;
            this.size = size;
            this.coordinates = coords;
            CalculateBounds();
        }

        public bool CheckPoint(Vector3 point)
        {
            bool result = bounds.Contains(point);
            return result;
        }

        public void CalculateBounds()
        {
            var center = position;
            bounds = new Bounds(center, size);
        }
    }
}
