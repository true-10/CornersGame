using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    #region fields
    [SerializeField] private Text _blackScore;
    [SerializeField] private Text _whiteScore;
    private Player _p1;
    private Player _p2;
    #endregion

    void Start()
    {
        GameObject _p1GO = GameObject.Find("Player 1 white");
        GameObject _p2GO = GameObject.Find("Player 2 black");
        _p1 = _p1GO.GetComponent<Player>();
        _p2 = _p2GO.GetComponent<Player>();
        _p1.OnEndMove += UpdateWhiteText;
        _p2.OnEndMove += UpdateBlackText;
    }

    void UpdateBlackText(int numb)
    {
        _blackScore.text = "Black: " + numb.ToString();
    }

    void UpdateWhiteText(int numb)
    {
        _whiteScore.text = "White: " + numb.ToString();
    }
}
