using UnityEngine;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;
using GridSystem;

namespace Corners
{

    public class ChipData
    {
        public PlayerColor color = PlayerColor.White;
        public bool _isAvalable = false;
    }

    public class Chip : MonoBehaviour, IPointerClickHandler
    {
        public PlayerColor Color = PlayerColor.White;
        [HideInInspector] public bool IsAvalable = false;
        [HideInInspector] public GridCell Holder; //на какой ячейке находится фишка
        [HideInInspector] public GridCell PrevHolder; //на какой ячейке находилась фишка

        public event Action<Chip> OnChipClick;

        private Transform cachedTransform;

        private ChipMover chipMover;

        private void Start()
        {
            cachedTransform = GetComponent<Transform>();
            chipMover = new(cachedTransform, GlobalConstants.Chips.MOVE_DURATION);
        }
        public void Move(Vector3 newPos)
        {
            chipMover.Move(newPos);
        }

        private void Scale(Vector3 newScale)
        {
            cachedTransform.DOScale(newScale, 0.1f);
        }

        public void SelectedScale()
        {
            //при выборе фишки увеличиваем ее
            Scale(GlobalConstants.Chips.SELECTED_SCALE);
        }

        public void DefaultScale()
        {
            Scale(Vector3.one);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsAvalable) return;
            OnChipClick?.Invoke(this);
        }

        public bool WasItJump()//вынести логику в другое место?
        {
            bool result = false;

            Vector3Int prevInds = PrevHolder.Coordinates;
            Vector3Int currentInds = Holder.Coordinates;

            if (Mathf.Abs(prevInds.x - currentInds.x) > 1) result = true;
            if (Mathf.Abs(prevInds.y - currentInds.y) > 1) result = true;

            return result;
        }

    }
}
