using Corners.UI;

namespace Corners
{
    public class StateEndGame : IState
    {
        #region fields

        private UIController uiController;
        private CheckEndGameProcessor checkEndGameProcessor;
         
        private PlayerManager playerManager;

        public int Type => (int)StateType.EndGame;

        #endregion

        public StateEndGame(UIController uiController, CheckEndGameProcessor checkEndGameProcessor, PlayerManager playerManager)
        {
            this.uiController = uiController;
            this.checkEndGameProcessor = checkEndGameProcessor;
            this.playerManager = playerManager;

        }

        public void OnEnter()
        {
            uiController.ShowEndGameMenu();
        }

        public void OnExit()
        {
            ResetGameParams();
        }

        void ResetGameParams()
        {
            var whitePlayer = playerManager.GetPlayer(PlayerColor.White);
            var blackPlayer = playerManager.GetPlayer(PlayerColor.Black);
            whitePlayer.ResetMoveCount();
            whitePlayer.ResetParams();
            blackPlayer.ResetMoveCount();
            blackPlayer.ResetParams();
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }
    }
}
