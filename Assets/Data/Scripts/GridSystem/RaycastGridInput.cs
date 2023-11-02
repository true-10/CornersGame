using System;
using UnityEngine;

namespace GridSystem
{
    public sealed class RaycastGridInput : IGridInput
    {
        public Action<GridCell> OnInput { get; set; }

        private IGridController gridController;

        private float distance;
        private string gridLayerName;
        private Camera cam;
        private Ray ray;

        public RaycastGridInput(IGridController gridController, string gridLayerName, float distance)
        {
            this.gridController = gridController;
            this.distance = distance;
            this.gridLayerName = gridLayerName;
            cam = Camera.main;
        }

        public void Tick()
        {
            var mousePos = Input.mousePosition;
            OnScreenRaycastUpdate(mousePos);
        }

        private void OnScreenRaycastUpdate(Vector3 mousePos)
        {
            if (gridController == null)
            {
                return;
            }
            ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out var hit, distance, 1 << LayerMask.NameToLayer(gridLayerName)))
            {
                Vector3 point = hit.point;
                point.y -= 0.05f;
                gridController.CheckPosition(point, OnInput);
            }
        }
    }

}
