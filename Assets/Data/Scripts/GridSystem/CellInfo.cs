namespace GridSystem
{
    [System.Serializable]
    public class CellInfo<T>
    {
        public GridCell GridCell;
       // public UnityEngine.GameObject Object;
        public T Object;

        public bool IsEmpty => Object == null;


    }
}

