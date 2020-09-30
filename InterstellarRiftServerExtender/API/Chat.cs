using IRSE.Managers.Handlers;
using IRSE.API.ResultObjects;
using NLog;
using System;
using System.Collections.Generic;

namespace IRSE.API
{
    public class Chat
    {
        private static Logger mainLog;

        internal static ChatHandler m_chatHandler;

        internal Chat(ChatHandler chatHandler)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            m_chatHandler = chatHandler;
        }

        /// <summary>
        /// Sends a message to all clients on the server
        /// </summary>
        /// <param name="message">The message to display</param>
        public static void BroadcastMessage(string message)
        {
            try
            {
                m_chatHandler.SendMessageFromServer("Server: " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
                mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Send a private message to the specified client by their SteamID
        /// </summary>
        /// <param name="id">Steam ID of the player</param>
        /// <param name="message">The message to Display</param>
        public static void SendPrivateMessage(ulong id, string message)
        {
            //todo
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
                mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sends a private message to the specified client by their player name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public static void SendPrivateMessage(string name, string message)
        {
            //todo
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
                mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());
            }
        }

        /// <summary>
        /// Returns every message in the ChatMessage list
        /// </summary>
        /// <returns>List of ChatMessage objects</returns>
        public static List<ChatMessage> GetChatMessages()
        {
            //todo
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSE [API] Call Error: " + ex.Message);
                mainLog.Error("IRSE [API] Call (" + ex.TargetSite + ") Exception: " + ex.ToString());
            }

            return new List<ChatMessage>();
        }
    }
}