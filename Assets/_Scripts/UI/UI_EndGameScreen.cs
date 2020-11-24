using UnityEngine;
using UnityEngine.UI;

public class UI_EndGameScreen : MonoBehaviour
{
    #region fields
    [SerializeField] private Text _winnerText;
    public bool _restartGame { get; private set; }
    public bool _mainMenu { get; private set; }
    #endregion
    
    void Start()
    {
        ResetMenu();
    }
    
    public void UpdateText(Player pl)
    {
        if( pl != null )
        {
            _winnerText.text = "The winner is " + pl.name;
        }
        else
        {
            _winnerText.text = "DRAW";
        }
        
    }

    public void ResetMenu()
    {
        _restartGame = false;
        _mainMenu = false;
    }

    public void RestartGame()
    {
        _restartGame = true;
    }

    public void GotoStartMenu()
    {
        _mainMenu = true;
    }
}
