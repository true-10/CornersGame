using UnityEngine;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Chip : MonoBehaviour, IPointerClickHandler
{
    #region fields
    public PlayerColor _color = PlayerColor.PC_White;
    [HideInInspector] public bool _isAvalable = false;
    [HideInInspector] public Cell _holder; //на какой ячейке находится фишка
    [HideInInspector] public Cell _prevHolder; //на какой ячейке находилась фишка

    public event Action<Chip> OnChipClick;
    private Vector3 _selectedScale = Vector3.one * 1.2f;

    private Transform _transform;
    #endregion

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    public void Move(Vector3 newPos)
    {
        _transform.DOMove(newPos, 0.3f);
    }

    void Scale(Vector3 newScale)
    {
        _transform.DOScale(newScale, 0.1f);
    }

    public void SelectedScale()
    {//при выборе фишки увеличиваем ее
        Scale(_selectedScale);
    }

    public void DefaultScale()
    {
        Scale(Vector3.one);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isAvalable) return;
        OnChipClick?.Invoke(this);
    }

    public bool WasItJump()
    {
        bool result = false;
        Vector2Int prevInds = _prevHolder.inds;
        Vector2Int currentInds = _holder.inds;

        if( Mathf.Abs( prevInds.x - currentInds.x ) > 1) result = true;
        if( Mathf.Abs( prevInds.y - currentInds.y ) > 1) result = true;

        return result;
    }
    
}
