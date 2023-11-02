using DG.Tweening;
using System;
using UnityEngine;

namespace Corners
{
    public sealed class ChipMover
    {
        private Transform chipTransform;
        private float duration;

        public ChipMover(Transform chipTransform, float duration)
        {
            this.chipTransform = chipTransform;
            this.duration = duration;
        }

        public void Move(Vector3 newPos, Action OnMoveComplete = null)
        {
            chipTransform.DOMove(newPos, duration);
        }
    }
}
