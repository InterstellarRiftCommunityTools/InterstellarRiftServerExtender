using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IRSE.Managers;
using IRSE.Managers.Handlers;
using NLog;
using Game.Server;

namespace IRSE.API
{
	public class Players
	{
		internal static PlayerHandler m_playerHandler;
        private static Logger mainLog;

        internal Players(PlayerHandler playerHandler)
		{
			m_playerHandler = playerHandler;
            mainLog = NLog.LogManager.GetCurrentClassLogger();
        }

		/// <summary>
		/// Gets all players on the server as Player objects.
		/// </summary>
		/// <returns> Player objects in a List of Player</returns>
		public static IEnumerable<Player> GetPlayers()
		{
			try
			{
				return (List<Player>)m_playerHandler.Players.AllPlayers();
			}
			catch (Exception ex)
			{
				Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
                mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());

				return null;
			}		
		}	
	}
}
