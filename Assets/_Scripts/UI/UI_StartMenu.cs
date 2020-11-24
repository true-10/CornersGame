using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartMenu : MonoBehaviour
{
    #region fields
    [SerializeField] private Dropdown _dropDown;
    [SerializeField] private Text _controlsText;
    public bool _startGame { get; private set;}
    #endregion
    
    public void OnUpdate()
    {

    }

    public MoveType GetMoveType()
    {
        MoveType mType = MoveType.MT_Simple;
        switch(_dropDown.value)
        {
            case 0: mType = MoveType.MT_Simple; break;
            case 1: mType = MoveType.MT_HV; break;
            case 2: mType = MoveType.MT_Checkers; break;
            default: mType = MoveType.MT_Simple; break;
        }
        return mType;
    }
   
    public void StartGame()
    {
        _startGame = true;
    }

    public void ResetMenu()
    {
        _startGame = false;
    }

    public void OnTypeChange()
    {
        if(GetMoveType() == MoveType.MT_Simple)
        {
            _controlsText.text = "LMB - move";
        }
        else
        {
            _controlsText.text = "LMB - move \n RMB - end move";
        }
    }
}
