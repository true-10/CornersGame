using System.Collections.Generic;
using System;
using System.Linq;

namespace Corners
{
    public sealed class PlayerManager
    {
        public Action<AbstractPlayer, int> OnMoveEnd { get; set; }

        private GameSettingsSO gameSettings;
        private List<AbstractPlayer> players;

        private GameMode gameMode;

        public PlayerManager(GameSettingsSO gameSettings)
        {
            this.gameSettings = gameSettings;
            gameMode = gameSettings.GameMode;
            InitPlayers();
        }

        private void OnPlayerMoveEnd(AbstractPlayer player, int count)
        {
            OnMoveEnd?.Invoke(player, count);
        }

        public void InitPlayers()
        {
            players = new();
            AbstractPlayer player = new Player()
            {
                Color = PlayerColor.White
            };
            player.OnEndMove -= OnPlayerMoveEnd;
            player.OnEndMove += OnPlayerMoveEnd;
            players.Add(player);
            if (gameMode == GameMode.WithPlayer)
            {
                player = new Player()
                {
                    Color = PlayerColor.Black
                };
            }
            else
            {
                player = new PlayerAI()
                {
                    Color = PlayerColor.Black
                };
            }
            player.OnEndMove -= OnPlayerMoveEnd;
            player.OnEndMove += OnPlayerMoveEnd;
            players.Add(player);
        }

        public AbstractPlayer GetPlayer(PlayerColor playerColor)
        {
            return players?.FirstOrDefault(x => x.Color == playerColor);
        }
    }
}
