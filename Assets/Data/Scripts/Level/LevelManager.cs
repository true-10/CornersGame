using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using Zenject;

namespace Corners
{
    public class LevelManager : MonoBehaviour
    {
        public List<Chip> WhiteChips { get; set; }
        public List<Chip> BlackChips { get; set; }
        private GridSystem.Grid grid { get => gridController.Grid; }

        [SerializeField]
        private Transform selectable;
        [SerializeField]
        private Vector3 selectableOffset = Vector3.up;
        [SerializeField]
        private Transform chipRoot;
        [SerializeField, Header("Flags")]
        private Transform whiteFlagTransform;
        [SerializeField]
        private Transform blackFlagTransform;

        [Inject]
        private StateMachine stateMachine;
        [Inject]
        private IGridController gridController;
        [Inject]
        private GridInfoManager<Chip> gridInfoManager;

        private FlagSwitcher flagSwitcher;
        private VisualSelectableCell visualSelectableCell;

        private ChipLoader chipLoader;
        private ChipSetter chipSetter;

        const int chipsCount = 9;
        const int offset = 3;

        public void Init(Action OnInitComplete)
        {
            gridInfoManager.Init(grid.Cells);
            FillFieldWithChips();
            flagSwitcher = new(whiteFlagTransform, blackFlagTransform, stateMachine);

            //перенести в плеермувстейт
            visualSelectableCell = new VisualSelectableCell(selectable, gridController, gridInfoManager, selectableOffset);

            OnInitComplete?.Invoke();
        }

        private void FillFieldWithChips()
        {
            chipLoader = new(chipRoot);

            WhiteChips = chipLoader.CreateListOfWhiteChips(chipsCount);
            BlackChips = chipLoader.CreateListOfBlackChips(chipsCount);

            chipSetter = new(grid, gridInfoManager);
            chipSetter.SetChipsOnGrid(WhiteChips, BlackChips, offset);
        }

        public void ResetLevel()
        {
            gridInfoManager.Init(grid.Cells);
            chipSetter.SetChipsOnGrid(WhiteChips, BlackChips, offset);
        }
    }
}
