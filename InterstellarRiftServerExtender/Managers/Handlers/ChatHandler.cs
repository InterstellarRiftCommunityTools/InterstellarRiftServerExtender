using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework.Networking;
using Game.Server;
using IRSE.API.ResultObjects;
using NLog;
using System;
using System.Collections.Generic;

namespace IRSE.Managers.Handlers
{
    public class ChatHandler
    {
        #region Fields

        private static Logger mainLog; //mainLog.Error
        private RPCDelegate m_delegate;

        private List<ChatMessage> m_chatMessageList;

        private ChatController m_chatController;

        #endregion Fields

        public List<ChatMessage> ChatMessages
        {
            get
            {
                if (m_chatMessageList == null)
                    m_chatMessageList = new List<ChatMessage>();

                return m_chatMessageList;
            }
            set
            {
                m_chatMessageList = value;
            }
        }

        public ChatHandler(Game.Server.ControllerManager controllerManager)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            try
            {
                mainLog.Info("IRSE: Loading ChatManager...");
                m_chatController = controllerManager.Chat;
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void SetupChatMessageHandler(NetworkHandler networkHandler)
        {
            try
            {
                Dictionary<Type, RPCDelegate> m_dict = networkHandler.Network.Net.RpcDispatcher.Functions;
                m_dict.TryGetValue(typeof(ClientChatMessage), out m_delegate);
                m_dict[typeof(ClientChatMessage)] = new RPCDelegate(OnPacketChatMessage);
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }


        #region Event Handlers

        // This method intercepts a server side event handler then activates the original, nothing is lost
        // if IRSE were to bug out, it will probally kill all chat on the game
        //
        protected void OnPacketChatMessage(RPCData data)
        {
            m_delegate.Invoke(data);

            try
            {
                Player player = ServerInstance.Instance.Handlers
                    .PlayerHandler.Players.GetPlayerByConnectionId((long)data.OriginalMessage.SenderConnectionId);

                string name = player.Name;
                ClientChatMessage obj = (ClientChatMessage)data.DeserializedObject;

                ChatMessages.Add(new ChatMessage(player.Name, obj.message, player.ID));

                string formatted = String.Format("[All]{0}({1}): {2}", player.Name, player.ID, obj.message);

                //if (obj.channel == "All" || obj.channel == "System") {
                mainLog.Info(formatted);
                Program.GUI.AddChatLine(formatted);
                //}
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        public void SendMessageFromServer(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            try
            {
                m_chatController.SendToAll(Config.Singleton.AllChatColor, msg);
            }
            catch (Exception)
            {
            }

            Program.GUI.AddChatLine(String.Format("{0} - {1}: {2}", DateTime.Now.ToLocalTime(), "Server", msg));
        }

        #endregion Event Handlers
    }
}