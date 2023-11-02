
namespace Corners
{
    public sealed class StatePlayerMove : IState
    {
        public int Type => (int)stateType;
        private StateType stateType = StateType.PlayerWhiteMove;

        public AbstractPlayer Player => player;
        private AbstractPlayer player;


        public StatePlayerMove(AbstractPlayer player)
        {
            this.player = player;
            if (player.Color == PlayerColor.Black)
            {
                stateType = StateType.PlayerBlackMove;
            }
        }

        public void OnEnter()
        {
            player.EnableChips(true);
        }

        public void OnExit()
        {
            player.ResetParams();
            player.EnableChips(false);
        }

        public void OnUpdate()
        {
            player.UpdateLogic();
        }

        public void OnFixedUpdate()
        {

        }
    }
}
