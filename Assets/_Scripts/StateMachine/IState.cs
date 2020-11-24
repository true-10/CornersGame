﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnUpdate();
    void OnFixedUpdate();
    void OnEnter();
    void OnExit();
}
