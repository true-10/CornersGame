using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState
{
    #region fields
    Player _player;
    #endregion

    public PlayerMoveState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.EnableChips(true);
        _player.ShowFlag(true);
    }

    public void OnExit()
    {
        _player.ResetParams();
        _player.EnableChips(false);
        _player.ShowFlag(false);
    }

    public void OnUpdate()
    {
        _player.OnUpdate();
    }

    public void OnFixedUpdate()
    {

    }
}
