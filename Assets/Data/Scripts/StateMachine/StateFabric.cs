using Corners.UI;
using System.Collections.Generic;

namespace Corners
{
    public sealed class StateFabric
    {
        private UIController uIController;
        private CheckEndGameProcessor checkEndGameProcessor;
        private PlayerManager playerManager;

        public StateFabric(UIController uIController, CheckEndGameProcessor checkEndGameProcessor, PlayerManager playerManager)
        {
            this.uIController = uIController;
            this.checkEndGameProcessor = checkEndGameProcessor;

            this.playerManager = playerManager;
        }

        public List<IState> GenerateStates()
        {
            List<IState> states = new();
            IState newState = new StateStartMenu(uIController);
            states.Add(newState);
            newState = new StatePlayerMove(playerManager.GetPlayer(PlayerColor.White));
            states.Add(newState);
            newState = new StatePlayerMove(playerManager.GetPlayer(PlayerColor.Black));
            states.Add(newState);
            newState = new StateEndGame(uIController, checkEndGameProcessor, playerManager);
            states.Add(newState);
            return states;
        }
    }
}

