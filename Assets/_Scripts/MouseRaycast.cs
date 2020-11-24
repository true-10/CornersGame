using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    #region fields
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _mask;

    private Ray _ray;                 
    private Vector3 _currentPos;      
    protected Vector3 _mousePos;
    #endregion

    void FixedUpdate()
    {
        _mousePos = Vector3.zero;
        _mousePos = Input.mousePosition;
        _ray = _cam.ScreenPointToRay(_mousePos);

        _currentPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, 200f, _mask)) 
        {
            _currentPos = hit.point;
        }
    }

    public Vector3 GetMousePosition()
    {
        return _currentPos;
    }
}
