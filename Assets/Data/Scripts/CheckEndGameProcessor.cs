using GridSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corners
{
    //класс проверки окончания игры
    public sealed class CheckEndGameProcessor
    {
        public Action<AbstractPlayer> OnEndGame { get; set; }

        private AbstractPlayer whitePlayer;
        private AbstractPlayer blackPlayer;

        //когда фишки одного соперника полностью окажутся на базе другого, то победа
        //после 80 хода черного игра останавливается и ведем подсчет
        //у кого фишек в базе врага больше, тот и победил
        //или ничья
        private List<GridCell> whiteStartCells;
        private List<GridCell> blackStartCells;

        private List<Chip> blackChips;
        private List<Chip> whiteChips;

        private int whiteChipsInBlackBase = 0;
        private int blackChipsInWhiteBase = 0;

        private const int moveLimit = 3;//79;

        public void Init(AbstractPlayer white, AbstractPlayer black)
        {
            whitePlayer = white;
            blackPlayer = black;
            whiteStartCells = new List<GridCell>();
            blackStartCells = new List<GridCell>();

            whiteChips = whitePlayer.GetChips();
            blackChips = blackPlayer.GetChips();
            //сохраняем ячейки базы
            foreach (Chip ch in whiteChips)
            {
                whiteStartCells.Add(ch.Holder);
            }
            foreach (Chip ch in blackChips)
            {
                blackStartCells.Add(ch.Holder);
            }
        }

        public AbstractPlayer GetWinner()
        {
            if (IsGameContinue())
            {
                return null;
            }
            AbstractPlayer winner = null;
            if (whiteChipsInBlackBase > blackChipsInWhiteBase) winner = whitePlayer;
            if (blackChipsInWhiteBase > whiteChipsInBlackBase) winner = blackPlayer;

            return winner;
        }

        public bool IsGameContinue()
        {
            bool result = true;
            if (whiteChipsInBlackBase == 9) result = false;
            if (blackChipsInWhiteBase == 9) result = false;
            if (OutOfMoves()) result = false;
            return result;
        }

        private bool OutOfMoves()
        {
            return blackPlayer.GetMoveCount() > moveLimit;
        }

        public bool IsEndGameConditionMet()
        {
            whiteChipsInBlackBase = 0;
            foreach (Chip ch in whiteChips)
            {
                if (blackStartCells.Contains(ch.Holder))
                {
                    whiteChipsInBlackBase++;
                }
            }
            blackChipsInWhiteBase = 0;
            foreach (Chip ch in blackChips)
            {
                if (whiteStartCells.Contains(ch.Holder))
                {
                    blackChipsInWhiteBase++;
                }
            }

            var winner = GetWinner();
            if (winner != null)
            {
                OnEndGame?.Invoke(winner);
                return true;
            }
            return false;
        }
    }

}
