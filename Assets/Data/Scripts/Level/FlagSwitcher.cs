using DG.Tweening;
using UnityEngine;

namespace Corners
{
    public class FlagSwitcher
    {
        private Transform whiteFlagTransform;
        private Transform blackFlagTransform;

        private Quaternion flagDefaultRotation;
        private StateMachine stateMachine;

        public FlagSwitcher(Transform whiteFlagTransform, Transform blackFlagTransform, StateMachine stateMachine)
        {
            this.whiteFlagTransform = whiteFlagTransform;
            this.blackFlagTransform = blackFlagTransform;
            this.stateMachine = stateMachine;

            flagDefaultRotation = whiteFlagTransform.rotation;

            stateMachine.OnStateEnter += OnStateEnter;
            stateMachine.OnStateExit += OnStateExit;
        }

        private void ShowFlag(bool show, Transform flagTransform)
        {
            if (show)
            {
                Quaternion newRot = Quaternion.Euler(-90f + flagDefaultRotation.eulerAngles.x, flagDefaultRotation.eulerAngles.y, flagDefaultRotation.eulerAngles.z);
                flagTransform.DORotateQuaternion(newRot, 0.5f);
            }
            else
            {
                flagTransform.DORotateQuaternion(flagDefaultRotation, 0.5f);
            }
        }

        private void OnStateEnter(IState state)
        {
            StateType stateType = (StateType)state.Type;
            switch (stateType)
            {
                case StateType.PlayerWhiteMove:
                case StateType.PlayerBlackMove:
                    {
                        var playerMoveState = state as StatePlayerMove;
                        var flagTransform = whiteFlagTransform;
                        if (playerMoveState.Player.Color == PlayerColor.Black)
                        {
                            flagTransform = blackFlagTransform;
                        }
                        ShowFlag(true, flagTransform);
                    }
                    break;
            }
        }

        private void OnStateExit(IState state)
        {
            StateType stateType = (StateType)state.Type;
            switch (stateType)
            {
                case StateType.PlayerWhiteMove:
                case StateType.PlayerBlackMove:
                    {
                        var playerMoveState = state as StatePlayerMove;
                        var flagTransform = whiteFlagTransform;
                        if (playerMoveState.Player.Color == PlayerColor.Black)
                        {
                            flagTransform = blackFlagTransform;
                        }
                        ShowFlag(false, flagTransform);
                    }
                    break;
            }
        }
    }
}
