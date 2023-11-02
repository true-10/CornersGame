using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Corners.UI
{

    public class UIStartMenu : MonoBehaviour
    {
        [SerializeField]
        private Dropdown dropDown;
        [SerializeField]
        private Text controlsText;
        [SerializeField] 
        private GameManager gameManager;

        private MoveType mType = MoveType.Simple;
        private GameMode gameMode = GameMode.WithPlayer;

        [Inject]
        private GameSettingsSO gameSettings;
      
        private void OnEnable()
        {
            dropDown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDisable()
        {
            dropDown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int value)
        {
            switch (dropDown.value)
            {
                case 0: mType = MoveType.Simple; break;
                case 1: mType = MoveType.HorizontalAndVertical; break;
                case 2: mType = MoveType.CheckersLike; break;
                default: mType = MoveType.Simple; break;
            }
            OnTypeChange();
        }

        public MoveType GetMoveType()
        {
            return mType;
        }

        public void StartGame()
        {
            gameManager.InitMoveProcessor(mType);
            gameManager.StartGame();
        }


        public void OnTypeChange()
        {
            if (GetMoveType() == MoveType.Simple)
            {
                controlsText.text = "LMB - move";
            }
            else
            {
                controlsText.text = "LMB - move \n RMB - end move";
            }

            gameSettings.GameMode = gameMode;
        }
    }
}
