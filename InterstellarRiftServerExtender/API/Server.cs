using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IRSE.Managers;
using IRSE.Managers.Handlers;
using NLog;
namespace IRSE.API
{
	public class Server
	{
        private static Logger mainLog; //mainLog.Error

        internal Server(HandlerManager handlerManager)
		{
            mainLog = NLog.LogManager.GetCurrentClassLogger();
        }

		/// <summary>
		/// Saves the galaxy!
		/// </summary>
		public static void SaveGalaxy()
		{
			try
			{
				ServerInstance.Instance.Handlers.ForceGalaxySave();
			}
			catch (Exception ex)
			{
				Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
				mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());
			}
			
		}
	}
}
