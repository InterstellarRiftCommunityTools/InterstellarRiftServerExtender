using Game.Configuration;
using Game.Server;
using NLog;
using System;

namespace IRSE.Managers.Plugins
{
    public class PluginHelper
    {
        private Game.Server.ControllerManager svr;
        private Logger logger;

        public Game.Server.ControllerManager GetControllers => svr;
        public Logger GetLogger => logger;

        public class Color
        {
            public static string AllChatColor => Config.Singleton.AllChatColor;
            public static string NotificationColor => Config.Singleton.NotificationChatColor;
        }

        public PluginHelper(Game.Server.ControllerManager controllers)
        {
            svr = controllers;

            logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="p"></param>
        /// <param name="message"></param>
        /// <param name="color">Color object, for </param>
        /// <param name="from"></param>
        public void SendMessageToClient(Player p, String message, Color color, String from = "")
        {
            GetControllers.Chat.SendToPlayer(p, color.ToString(), message, from, p.ID, "Server", -1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <param name="message"></param>
        public void SendMessageToServer(Color color, String message)
        {
            GetControllers.Chat.SendToAll(color.ToString(), message);
        }

        /// <summary>
        /// Gets the exact player from their full name
        /// </summary>
        /// <param name="name">EXACT name of person</param>
        /// <returns>Player</returns>
        public Player GetPlayerExact(String name)
        {
            foreach (Player player in GetControllers.Players.AllPlayers())
            {
                if (player.Name.ToLower() == name.ToLower()) return player;
            }
            return null;
        }

        /// <summary>
        /// Gets the exact player from their SteamID
        /// </summary>
        /// <param name="id">SteamID of the Player</param>
        /// <returns>Player</returns>
        public Player GetPlayerFromID(ulong id)
        {
            foreach (Player player in GetControllers.Players.AllPlayers())
            {
                if (player.ID == id) return player;
            }
            return null;
        }

        /// <summary>
        /// Attempts to find a player by their partial name
        /// </summary>
        /// <param name="name">Partial name of player</param>
        /// <returns>Player</returns>
        public Player GetPlayerFromPartialName(String name)
        {
            Player found = null;
            int delta = int.MaxValue;
            foreach (Player player in GetControllers.Players.AllPlayers())
            {
                if (player.Name.ToLower().StartsWith(name))
                {
                    int curDelta = player.Name.Length - name.Length;
                    if (curDelta < delta)
                    {
                        found = player;
                        delta = curDelta;
                    }
                    if (curDelta == 0)
                    {
                        break;
                    }
                }
            }
            return found;
        }
    }
}