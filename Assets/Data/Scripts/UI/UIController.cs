using UnityEngine;

namespace Corners.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private UIStartMenu startMenuUI;
        [SerializeField]
        private UIGame gameUI;
        [SerializeField]
        private UIEndGameScreen endGameUI;

        public UIStartMenu StartMenuUI => startMenuUI;
        public UIGame GameUI => gameUI;
        public UIEndGameScreen EndGameUI => endGameUI;

        public void ShowStartGameMenu()
        {
            HideAll();
            startMenuUI.gameObject.SetActive(true);
        }

        public void ShowGameMenu()
        {
            HideAll();
            gameUI.gameObject.SetActive(true);
        }

        public void ShowEndGameMenu()
        {
            HideAll();
            endGameUI.gameObject.SetActive(true);
        }

        private void HideAll()
        {
            startMenuUI.gameObject.SetActive(false);
            gameUI.gameObject.SetActive(false);
            endGameUI.gameObject.SetActive(false);
        }
    }
}
