using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GridSystem
{
    public class Sectors : IGrid
    {
        public class SectorsData
        {
            public List<GridCell> GridCells;
            public Vector3Int SectorSize;
            public Vector3Int GridSize;
        }

        private Dictionary<int, List<GridCell>> sectorsDictionary = new();

        private List<GridSector> sectors;
        private List<GridCell> gridCells;

        private Vector3Int sectorSize = Vector3Int.one * 3;

        public Vector3Int GridSize { get; private set; }

        public Sectors(SectorsData sectorsData)
        {
            this.gridCells = sectorsData.GridCells;
            this.sectorSize = sectorsData.SectorSize;
            this.GridSize = sectorsData.GridSize;

            CalculateSectors();
        }

        public GridCell GetCellByPosition(Vector3 point)
        {
            foreach (var sector in sectors)
            {
                var cell = sector.GetCellByPosition(point);
                if (cell != null)
                {
                    return cell;
                }
            }
            return null;
        }

      
        private void CalculateSectors()
        {
            //по количеству клеток делать секторы? типа 2 на 2
            sectors = new List<GridSector>();
            //Debug.Log($"CalculateSectors");
            int sectorIndex = 0;
           // int sectorCellIndexX = 0;
            //int sectorCellIndexY = 0;
            int sectorNumb = gridCells.Count / (sectorSize.x * sectorSize.z);
            Debug.Log($"Sectors: sectorSize ={sectorSize} | gridSize={GridSize}");

            for (int i = 0; i < gridCells.Count; i++)
            {
                //TODO
                //надо доделать под 3д
                GridCell cell = gridCells[i];
                int x = i % GridSize.x;
                int z = i / GridSize.x;
                //int y = i / GridSize.x;

                sectorIndex = 0;
                int sectorX = x / sectorSize.x;
                //  int sectorY = y / sectorSize.y;
                int sectorZ = z / sectorSize.z;

                sectorIndex = sectorX + sectorZ * sectorSize.x;

                AddCellToSector(cell, sectorIndex);
                // Debug.Log($"i={i} | x={x} y={y} | sectorX={sectorX} sectorY={sectorY} | sectorIndex={sectorIndex}");
            }

            foreach (var s in sectorsDictionary)
            {
                var sector = new GridSector(s.Value, s.Key, GridSize);

                sectors.Add(sector);
            }
        }

        private void AddCellToSector(GridCell cell, int sectorIndex)
        {
            if (sectorsDictionary.ContainsKey(sectorIndex))
            {
                var sectorCells = sectorsDictionary[sectorIndex];
                sectorCells.Add(cell);
            }
            else
            {
                List<GridCell> newSectorCells = new();
                newSectorCells.Add(cell);
                sectorsDictionary.Add(sectorIndex, newSectorCells);
            }
        }

        public GridCell GetCellFromIndicies(int xInd, int yInd)
        {
            return gridCells.FirstOrDefault(cell => cell.Coordinates.x == xInd && cell.Coordinates.z == yInd);
        }
    }
}
