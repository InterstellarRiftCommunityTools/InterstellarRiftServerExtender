using IRSE.Managers;
using NLog;
using System;

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