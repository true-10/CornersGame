using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using GridSystem;

namespace Corners
{
    public enum StateType
    {
        StartMenu = 0,
        EndGame = 1,
        PlayerWhiteMove = 2,
        PlayerBlackMove = 3,

    }

    public class GameManager : MonoBehaviour
    {
        #region fields
        [SerializeField]
        private LevelManager levelManager;

        [Inject]
        private IGridController gridController;
        [Inject]
        private StateMachine stateMachine;
        [Inject]
        private PlayerManager playerManager;
        [Inject]
        private GridInfoManager<Chip> gridInfoManager;
        [Inject]
        private CheckEndGameProcessor checkEndGameProcessor;

        private AbstractPlayer playerWhite;
        private AbstractPlayer playerBlack;
        private AbstractMove moveProcessor;
        private MoveType moveType;

        private FlagSwitcher flagSwitcher;

        private IGridInput gridInput;

        #endregion

        void Start()
        {
            playerWhite = playerManager.GetPlayer(PlayerColor.White);
            playerBlack = playerManager.GetPlayer(PlayerColor.Black);

            playerManager.OnMoveEnd += OnPlayerMoveEnd;
            stateMachine.OnStateEnter += OnStateEnter;
            stateMachine.OnStateExit += OnStateExit;
            checkEndGameProcessor.OnEndGame += OnEndGame;

            levelManager.Init(() =>
            {
                playerWhite.SetChips(levelManager.WhiteChips);
                playerBlack.SetChips(levelManager.BlackChips);
                checkEndGameProcessor.Init(playerWhite, playerBlack);
            });


            gridInput = new RaycastGridInput(gridController, gridLayerName: GlobalConstants.LayersName.GRID_LAYER, GlobalConstants.SomeNumbers.RAYCAST_DISTANCE);

            Init();

            StartMenu();
        }

        private void OnPlayerMoveEnd(AbstractPlayer player, int count)
        {
            if (checkEndGameProcessor.IsEndGameConditionMet())
            {
                Debug.Log("if (checkEndGameProcessor.IsEndGameConditionMet()) return");
                return;
            }
            StateType stateType = StateType.PlayerWhiteMove;
            if (player.Color == PlayerColor.White)
            {
                stateType = StateType.PlayerBlackMove;
            }
            stateMachine?.SetState((int)stateType);
        }

        private void OnEndGame(AbstractPlayer winner)
        {
            stateMachine?.SetState((int)StateType.EndGame);
        }

        private void OnDestroy()
        {
            gridInput = null;
            if (stateMachine != null)
            {
                stateMachine.OnStateEnter -= OnStateEnter;
                stateMachine.OnStateExit -= OnStateExit;
            }
            if (checkEndGameProcessor != null)
            {
                checkEndGameProcessor.OnEndGame -= OnEndGame;
                checkEndGameProcessor = null;
            }
            if (playerManager != null)
            {
                playerManager.OnMoveEnd -= OnPlayerMoveEnd;
            }
        }

        public void Init()
        {
            InitMoveProcessor(moveType);
            playerWhite.Init(gridController, gridInput, moveProcessor, gridInfoManager);
            playerBlack.Init(gridController, gridInput, moveProcessor, gridInfoManager);
        }

        public void StartGame()
        {
            levelManager.ResetLevel();
            playerWhite.ResetParams();
            playerWhite.ResetMoveCount();
            playerBlack.ResetParams();
            playerBlack.ResetMoveCount();

            stateMachine?.SetState((int)StateType.PlayerWhiteMove);
        }

        public void StartMenu()
        {
            stateMachine?.SetState((int)StateType.StartMenu);
        }

        void Update()
        {
            gridInput?.Tick();
            stateMachine?.Tick();
        }

        private void OnStateEnter(IState state)
        {
            StateType stateType = (StateType)state.Type;
            switch (stateType)
            {
                case StateType.StartMenu: break;
                case StateType.PlayerWhiteMove: break;
                case StateType.PlayerBlackMove: break;
                case StateType.EndGame: break;
            }
        }

        private void OnStateExit(IState state)
        {
            StateType stateType = (StateType)state.Type;
            switch (stateType)
            {
                case StateType.StartMenu: break;
                case StateType.PlayerWhiteMove: break;
                case StateType.PlayerBlackMove: break;
                case StateType.EndGame: break;
            }
        }

        public void InitMoveProcessor(MoveType moveType)
        {
            this.moveType = moveType;
            switch (moveType)
            {
                case MoveType.Simple: moveProcessor = new MoveSimple(); break;
                case MoveType.HorizontalAndVertical: moveProcessor = new MoveHV(); break;
                case MoveType.CheckersLike: moveProcessor = new MoveChekers(); break;
                default: moveProcessor = new MoveSimple(); break;
            }
        }
    }
}
