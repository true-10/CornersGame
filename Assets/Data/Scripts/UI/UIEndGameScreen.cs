using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Corners.UI
{
    public class UIEndGameScreen : MonoBehaviour
    {
        [SerializeField] private Text _winnerText;

        [Inject]
        private CheckEndGameProcessor checkEndGameProcessor;


        void OnEnable()
        {
            checkEndGameProcessor.OnEndGame += UpdateText;
        }

        private void OnDisable()
        {
            checkEndGameProcessor.OnEndGame -= UpdateText;
        }

        private void UpdateText(AbstractPlayer pl)
        {
            if (pl != null)
            {
                _winnerText.text = $"The winner is {pl.Color}";
            }
            else
            {
                _winnerText.text = "DRAW";
            }

        }
    }
}
