using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
/*
[System.Serializable]
public class Cell
{
    public bool _isEmpty;
    public Chip _chip; //какая фишка стоит на ячейке
    public Vector2Int inds;//индексы в массиве
    public Vector3 position;
}

public class Grid : MonoBehaviour, IPointerExitHandler
{
    #region fields
    [SerializeField] private float cellSize = 1f;//размер ячейки
    public bool _canSelect = false;
    private Cell[,] cells;
    private Cell selectedCell;//ячейка, на которую навели курсор
    [SerializeField] private int _rowCount = 8;//кол-во строк
    [SerializeField] private int _colCount = 8;//кол-во столбцов
    [SerializeField] private float _yHeight= 1f;//высота положения _selectedQuad


    [SerializeField] private Transform _selectedQuad;
    [SerializeField] private MouseRaycast _mouseRaycast;

    private float _offset = 3.5f;//сдвиг от центра

    private List<Cell> _availableMoves;//доступные для хода ячейки
    #endregion

    void Start()
    {
        CreateCellsArray();
    }

    public Cell GetSelectedCell()
    {
        if (_availableMoves.Contains(selectedCell))
        {
            return selectedCell;
        }
        return null;
    }

    public void OnUpdate()
    {
        if (_canSelect == false) return;
        if (_availableMoves.Count == 0)
        {
            return;
        }
        
        selectedCell = GetCell();
        if (_availableMoves.Contains(selectedCell))
        {            
            _selectedQuad.transform.position = selectedCell.position;
        }
        else
        {            
            HideSelectedQuad();
        }
    }

    void CreateCellsArray()
    {
        cells = new Cell[_rowCount, _colCount];
        for (int x = 0; x < _rowCount; x++)
        {
            for (int y = 0; y < _colCount; y++)
            {
                Cell cell =  new Cell();
                cell._isEmpty = true;
                cell._chip = null;
                cell.inds = new Vector2Int(x, y);
                cell.position = new Vector3(x * cellSize - _offset, _yHeight, y * cellSize - _offset);
                cells[x, y] = cell;
            }
        }
    }

    public void ResetCells()
    {
        if( cells == null )
        {
            return;
        }
        for (int x = 0; x < _rowCount; x++)
        {
            for (int y = 0; y < _colCount; y++)
            {
                Cell cell = GetCellFromIndicies(x, y);
                cell._isEmpty = true;
                cell._chip = null;
            }
        }
    }
    
    Cell GetCell()
    {
        Vector3 mousePos = Vector3.zero;
        mousePos = _mouseRaycast.GetMousePosition();
        return GetCellFromPosition(mousePos);
    }
    
    Cell GetCellFromPosition(Vector3 pos)
    {
        Cell cell = null;
        Vector2Int inds = GetCellIndexes(pos.x, pos.z);
        cell = cells[inds.x, inds.y];
        return cell;
    }

    public Cell GetCellFromIndicies(int x, int y)
    {
        //проверяем на корректность индексов
        if (x < 0 || x > _rowCount - 1) return null;
        if (y < 0 || y > _colCount - 1) return null;
        return cells[x, y];
    }

    public Vector2Int GetCellIndexes(float xPos, float zPos)
    {
        Vector2Int inds = Vector2Int.zero;
        inds.x = Mathf.Clamp( Mathf.RoundToInt(xPos + _offset / cellSize), 0, _rowCount - 1);
        inds.y = Mathf.Clamp( Mathf.RoundToInt(zPos + _offset / cellSize), 0, _colCount - 1);
        return inds;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectedCell = null;
    }

    public void HideSelectedQuad()
    {
        selectedCell = null;
        _selectedQuad.transform.position = Vector3.zero;
    }

    public void SetWhiteChipsOnField(List<Chip> chips, int offset)
    {
        int chipIndex = 0;
        for (int x = 0; x <  offset; x++)
        {
            for (int y = 0; y < offset; y++)
            {
                Chip chip = chips[chipIndex];
                Cell cell = cells[x, y];
                chip._holder = cell;
                chip._prevHolder = cell;
                cell._chip = chip;
                cell._isEmpty = false;
                chip.transform.position = cell.position;
                chipIndex++;
            }
        }
    }

    public void SetBlackChipsOnField(List<Chip> chips, int offset)
    {
        int chipIndex = 0;
        for (int x = _rowCount - 1; x > _rowCount - 1 - offset; x--)
        {
            for (int y = _colCount - 1; y > _colCount - 1 - offset; y--)
            {
                Chip chip = chips[chipIndex];
                Cell cell = cells[x, y];
                chip._holder = cell;
                chip._prevHolder = cell;
                cell._chip = chip;
                cell._isEmpty = false;
                chip.transform.position = cell.position;
                chipIndex++;
            }
        }
    }

    public int GetRowCount()
    {
        return _rowCount;
    }

    public int GetColCount()
    {
        return _colCount;
    }
    
    public void CheckMoveVariations(AbstractMove processor, Cell curretnCell)
    {
        _availableMoves = processor.CheckVariations(curretnCell, this);
    }
    
    public bool IsNoMoreMoves()
    {
        return _availableMoves.Count == 0;
    }
}
*/