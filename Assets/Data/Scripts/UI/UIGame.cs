using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Corners.UI
{

    public class UIGame : MonoBehaviour
    {
        [SerializeField]
        private Text whiteScore;
        [SerializeField]
        private Text blackScore;

        [Inject]
        private PlayerManager playerManager;

        void OnEnable()
        {
            playerManager.OnMoveEnd += UpdatePlayersText;
        }

        private void OnDisable()
        {
            playerManager.OnMoveEnd -= UpdatePlayersText;
        }

        void UpdatePlayersText(AbstractPlayer player, int numb)
        {
            if (player.Color == PlayerColor.Black)
            {
                blackScore.text = $"Black: {numb}";
            }
            else
            {
                whiteScore.text = $"White: {numb}";
            }
        }

    }

}
