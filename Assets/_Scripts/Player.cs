using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public enum PlayerColor
{
    PC_White,
    PC_Black
}

public class Player : MonoBehaviour
{
    #region fields
    [SerializeField] Transform _flag;
    private Quaternion _defaultRotation;
    private List<Chip> _chips; //фишки принадлежащие этому игроку
    private Chip _activeChip; //выделенная фишка
    private Grid _grid;
    private int _moveCount = 0; //кол-во ходов
    private int _movePerTurn = 0; //кол-во движений фишки за ход
    public bool _endMove { get; private set; }
    public event Action<int> OnEndMove;

    private Move _moveProcessor;
    #endregion

    public void Start()
    {
        _defaultRotation = _flag.rotation;
    }

    public void Init(Grid grid, Move moveProcessor)
    {
        _grid = grid;
        _moveCount = 0;
        _activeChip = null;
        _moveProcessor = moveProcessor;
        OnEndMove?.Invoke(_moveCount);
    }

    public void ResetParams()
    {
        _activeChip = null;
        _endMove = false;
        _movePerTurn = 0;
        EnableChips(false);
    }

    public void OnUpdate()
    {
        _grid.OnUpdate();
        //лкм
        if (Input.GetMouseButtonDown(0) )
        {
            if (_activeChip != null)
            {
                Cell activeCell = _grid.GetSelectedCell();
                if (activeCell == null) return;
                MoveChip(activeCell);
            }
        }
        //пкм - завершение хода
        if( !_moveProcessor.IsMoveComplete() )
            if (Input.GetMouseButtonDown(1))
            {
                if(_movePerTurn > 0)
                {//нельзя завершить ход, если не двинул фишку
                    EndMove();
                }                
            }
    }

    void SetActiveChip(Chip chip)
    {
        if(_activeChip != null)
        {//если какая то фишка была выделена до этого, то скейлим обратно
            _activeChip.DefaultScale();
        }
        _activeChip = chip;

        _moveProcessor.ResetLongMove();
        _grid.CheckMoveVariations(_moveProcessor, _activeChip._holder);
        _activeChip.SelectedScale();
        _grid._canSelect = true;
    }

    public void MoveChip(Cell cell)
    {
        if (!cell._isEmpty) return;
        EnableChips(false);//выключаем другие фишки, что бы нельзя было их выбрать
        _movePerTurn++;
        _activeChip._holder._isEmpty = true;
        _activeChip._prevHolder = _activeChip._holder;
        
        _activeChip._holder._chip = null;
        _activeChip.Move(cell.position);

        _activeChip._holder = cell;
        _activeChip._holder._isEmpty = false;
        _activeChip._holder._chip = _activeChip;
        _grid.HideSelectedQuad();
       
        if (_moveProcessor.IsMoveComplete() )
        {
            EndMove();
            return;
        }
        else
        {
            _grid.CheckMoveVariations(_moveProcessor, _activeChip._holder);
        }
        if(! _activeChip.WasItJump())
        {
            EndMove();
            return;
        }
        if (_grid.IsNoMoreMoves())
        {
            EndMove();
            return;
        }

    }

    public void EnableChips(bool available)
    {
        foreach(Chip ch in _chips)
        {
            ch._isAvalable = available;
        }
    }

    public void SetChips(List<Chip> chipList)
    {
        _chips = chipList;

        foreach (Chip ch in _chips)
        {
            ch.OnChipClick += SetActiveChip;
        }
    }

    public List<Chip> GetChips()
    {
        return _chips;
    }

    public int GetMoveCount()
    {
        return _moveCount;
    }

    public void RemoveChips()
    {
        foreach (Chip ch in _chips)
        {
            ch.OnChipClick -= SetActiveChip;
        }
        _chips.Clear();
    }

    public void ResetMoveCount()
    {
        _moveCount = 0;
        OnEndMove?.Invoke(_moveCount);     
    }

    public void ShowFlag(bool show)
    {
        if( show )
        {
            Quaternion newRot = Quaternion.Euler(-90f + _defaultRotation.eulerAngles.x, _defaultRotation.eulerAngles.y, _defaultRotation.eulerAngles.z);
            _flag.DORotateQuaternion(newRot, 0.5f);
        }
        else
        {
            _flag.DORotateQuaternion(_defaultRotation, 0.5f);
        }
    }

    public void EndMove()
    {
        _grid._canSelect = false;
        _grid.HideSelectedQuad();
        _activeChip.DefaultScale();
        _activeChip = null;
        _moveCount++;
        _endMove = true;
        _moveProcessor.ResetLongMove();
        OnEndMove?.Invoke(_moveCount);
    }
}
