using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    int Type { get; }
    void OnUpdate();
    void OnFixedUpdate();
    void OnEnter();
    void OnExit();
}
