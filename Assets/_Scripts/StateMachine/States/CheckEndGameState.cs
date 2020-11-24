using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckEndGameState : IState
{
    #region fields
        CheckEndGame _processor;
    #endregion

    public CheckEndGameState(CheckEndGame processor)
    {
        _processor = processor;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        _processor.CheckChipsPosition();
    }

    public void OnFixedUpdate()
    {

    }
}

