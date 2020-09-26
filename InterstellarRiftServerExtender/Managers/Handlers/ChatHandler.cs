using Game.ClientServer.Packets;
using Game.Configuration;
using Game.Framework.Networking;
using Game.Server;
using IRSE.ResultObjects;
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

        public bool ParseCommand(Player ply, string msg)
        {
            /*
			// Motd request
			Match motdmatch = Regex.Match(msg, "^/[mM]otd");
			if (motdmatch.Success && motdmatch.Groups.Count == 1)
			{
				try
				{
					ply.SendRPC((object)new ServerChatMessage("~-C" + (object)Game.Configuration.Config.Singleton.NotificationChatColor + " " + Config.Settings.OnJoinMotd.Replace("#name#", ply.Name)));
				}
				catch (Exception)
				{
					return false;
				}

				return true;
			}

			// Admin commands
			if (Config.Settings.GameAdminsSteamID.Contains(ply.ID))
			{
				// Give
				Match givematch = Regex.Match(msg, "^/[gG]ive \"([^\"]+)\" \"([^\"]+)\" (\\d+)");
				if (givematch.Success && givematch.Groups.Count == 4)
				{
					string name = givematch.Groups[1].Value.ToLower();
					string str = givematch.Groups[2].Value;
					int num = Convert.ToInt32(givematch.Groups[3].Value);

					ClSvEconomics.resourceTy .ResourceTypes resourceTypes;
					try
					{
						resourceTypes = (ClSvEconomics.ResourceTypes)Enum.Parse(typeof(ClSvEconomics.ResourceTypes), str);
					}
					catch
					{
						return false;
					}

					Player playerByName = ServerInstance.Instance.Handlers
						.PlayerHandler.Players.GetPlayerByName(name);
					if (playerByName != null)
					{
						Player player = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate = new ClSvPlayerResourceCrate();
						playerResourceCrate.amount = num;
						playerResourceCrate.resourceType = resourceTypes;
						ClSvPlayerResourceCrate gainedResource = playerResourceCrate;
						player.AddResourceToVault(gainedResource);

						ply.SendRPC((object)new ServerChatMessage("~-C" + (object)Game.Configuration.Config.Singleton.NotificationChatColor + "Gave " + num + " " + str + " to \"" + name + "\"", ""));
					}
					else
						ply.SendRPC((object)new ServerChatMessage("~-C" + Game.Configuration.Config.Singleton.NotificationChatColor + "Player \"" + name + "\" does not exist", ""));

					return true;
				}

				Match match = Regex.Match(msg, "^/[gG]iveallres \"([^\"]+)");
				if (match.Groups.Count == 2)
				{
					string name = match.Groups[1].Value.ToLower();
					Player playerByName = ServerInstance.Instance.Handlers
						.PlayerHandler.Players.GetPlayerByName(name);
					if (playerByName != null)
					{
						Player player1 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate1 = new ClSvPlayerResourceCrate();
						playerResourceCrate1.amount = 200000;
						playerResourceCrate1.resourceType = ClSvEconomics.ResourceTypes.RT_Iron;
						ClSvPlayerResourceCrate gainedResource1 = playerResourceCrate1;
						player1.AddResourceToVault(gainedResource1);
						Player player2 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate2 = new ClSvPlayerResourceCrate();
						playerResourceCrate2.amount = 200000;
						playerResourceCrate2.resourceType = ClSvEconomics.ResourceTypes.RT_Hydrogen;
						ClSvPlayerResourceCrate gainedResource2 = playerResourceCrate2;
						player2.AddResourceToVault(gainedResource2);
						Player player3 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate3 = new ClSvPlayerResourceCrate();
						playerResourceCrate3.amount = 200000;
						playerResourceCrate3.resourceType = ClSvEconomics.ResourceTypes.RT_Copper;
						ClSvPlayerResourceCrate gainedResource3 = playerResourceCrate3;
						player3.AddResourceToVault(gainedResource3);
						Player player4 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate4 = new ClSvPlayerResourceCrate();
						playerResourceCrate4.amount = 200000;
						playerResourceCrate4.resourceType = ClSvEconomics.ResourceTypes.RT_Oxygen;
						ClSvPlayerResourceCrate gainedResource4 = playerResourceCrate4;
						player4.AddResourceToVault(gainedResource4);
						Player player5 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate5 = new ClSvPlayerResourceCrate();
						playerResourceCrate5.amount = 200000;
						playerResourceCrate5.resourceType = ClSvEconomics.ResourceTypes.RT_Silicon;
						ClSvPlayerResourceCrate gainedResource5 = playerResourceCrate5;
						player5.AddResourceToVault(gainedResource5);
						Player player6 = playerByName;
						ClSvPlayerResourceCrate playerResourceCrate6 = new ClSvPlayerResourceCrate();
						playerResourceCrate6.amount = 200000;
						playerResourceCrate6.resourceType = ClSvEconomics.ResourceTypes.RT_Unobtainium;
						ClSvPlayerResourceCrate gainedResource6 = playerResourceCrate6;
						player6.AddResourceToVault(gainedResource6);
						ply.SendRPC((object)new ServerChatMessage("~-C" + Game.Configuration.Config.Singleton.NotificationChatColor + "Gave all resources", ""));
					}
					else
						ply.SendRPC((object)new ServerChatMessage("~-C" + Game.Configuration.Config.Singleton.NotificationChatColor + "Player \"" + name + "\" does not exist", ""));

					return true;
				}
        }
				*/
            return false;
        }

        #region Event Handlers

        // This method intercepts a server side event handler then activates the original, nothing is lost
        // if IRSE were to bug out, it will probally kill all chat on the game
        //
        protected void OnPacketChatMessage(RPCData data)
        {
            try
            {
                Player player = ServerInstance.Instance.Handlers
                    .PlayerHandler.Players.GetPlayerByConnectionId((long)data.OriginalMessage.SenderConnectionId);

                string name = player.Name;
                string message = ((ClientChatMessage)data.DeserializedObject).message;

                if (ParseCommand(player, message))
                {
                    mainLog.Info("'" + name + "' used command " + message);
                    return;
                }

                m_delegate.Invoke(data);

                ChatMessages.Add(new ChatMessage(name, message));
                mainLog.Info(String.Format("[Chat]{0}: {1}", name, message));
            }
            catch (Exception ex)
            {
                mainLog.Error(ex.ToString());
            }
        }

        #endregion Event Handlers

        #region Methods

        public void SendMessageFromServer(string messageToSend)
        {
			m_chatController.SendToAll(Config.Singleton.AllChatColor, messageToSend, "Server");
		}

        #endregion Methods
    }
}