using Discord.WebSocket;
using Game.ClientServer.Packets;
using Game.Server;
using IRSE.Managers;
using IRSE.Managers.Events;
using IRSE.Managers.Plugins;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRSEDiscordChatBot
{
    [Plugin(Name = "Discord Chat Relay", Version = "0.4.2", Author = "generalwrex", Description = "Links Chat to/from discord via a bot")]
    public class PluginMain : PluginBase
    {
        private static bool debugMode;
        private static MyConfig MyConfig;

        private static Form form;
        public Form PluginControlForm => form == null ? form = new Form1() : form;

        public override void OnLoad(string directory)
        {
            try
            {
                MyConfig.FileName = Path.Combine(directory, "Config.xml");
                MyConfig = new MyConfig();


                debugMode = MyConfig.Settings.DebugMode;
            }
            catch (Exception ex)
            {
                GetLogger.Warn(ex, "IRSEDiscordChatBot Initialization failed.");
                Console.WriteLine(ex.ToString());
            }
        }

        public override void Shutdown()
        {
            if(DiscordClient.SocketClient.ConnectionState == Discord.ConnectionState.Connected)
                DiscordClient.Instance.StopBot();

            DiscordClient.Instance = null;
        }

        public override void Init()
        {
            try
            {
                new DiscordClient();

                DiscordClient.SocketClient.MessageReceived += SocketClient_MessageReceived;

                DiscordClient.Instance.Start();
            }
            catch (Exception ex1)
            {
                GetLogger.Warn(ex1, "IRSEDiscordChatBot Discord Initialization failed.");
                Console.WriteLine(ex1.ToString());
            }
        }

        [IRSEEvent(EventType = typeof(ClientRespawn))]
        public void OnPacketRespawn(GenericEvent evt)
        {
            if (MyConfig.Instance.Settings.PlayerRespawningMessage.StartsWith("null")) return;

            if (debugMode)
                Console.WriteLine($"<- Sending Respawn Message To Discord");

            Player player = evt.Data.DeserializedObject as Player;

            if (player == null)
                return;

            string outMsg = MyConfig.Instance.Settings.PlayerRespawningMessage.Replace("(%PlayerName%)", player.Name).Replace("(%CurrentDateTime%)", DateTime.Now.ToString())
                ?? $"Player '{player.Name}' is respawning on the game server.";

            var channel = MyConfig.Instance.Settings.CDCChannelID != 0
                ? MyConfig.Instance.Settings.CDCChannelID
                : MyConfig.Instance.Settings.MainChannelID;

            DiscordClient.SendMessageToChannel(channel, outMsg);
        }

        [IRSEEvent(EventType = typeof(ClientConnected))]
        public void Players_OnAddPlayer(GenericEvent evt)
        {
            if (MyConfig.Instance.Settings.PlayerSpawningMessage.StartsWith("null")) return;

            if (debugMode)
                Console.WriteLine($"<- Sending Player Spawn Message To Discord");

            Player player = evt.Data.DeserializedObject as Player;

            if (player == null)
                return;

            string outMsg = MyConfig.Instance.Settings.PlayerSpawningMessage.Replace("(%PlayerName%)", player.Name).Replace("(%CurrentDateTime%)", DateTime.Now.ToString())
                ?? $"A player '{player.Name}' has connected to the game server.";

            var channel = MyConfig.Instance.Settings.CDCChannelID != 0
                ? MyConfig.Instance.Settings.CDCChannelID
                : MyConfig.Instance.Settings.MainChannelID;

            DiscordClient.SendMessageToChannel(channel, outMsg);
        }

        [IRSEEvent(EventType = typeof(ClientDisconnected))]
        public void Players_OnRemovePlayer(GenericEvent evt)
        {
            if (MyConfig.Instance.Settings.PlayerLeavingMessage.StartsWith("null")) return;

            if (debugMode)
                Console.WriteLine($"<- Sending Disconnect Message To Discord");

            Player player = evt.Data.DeserializedObject as Player;

            if (player == null)
                return;

            string outMsg = MyConfig.Instance.Settings.PlayerLeavingMessage.Replace("(%PlayerName%)", player.Name).Replace("(%CurrentDateTime%)", DateTime.Now.ToString())
                ?? $"{player.Name} disconnected from the game server.";

            var channel = MyConfig.Instance.Settings.CDCChannelID != 0
                ? MyConfig.Instance.Settings.CDCChannelID
                : MyConfig.Instance.Settings.MainChannelID;

            DiscordClient.SendMessageToChannel(channel, outMsg);
        }

        [IRSEEvent(EventType = typeof(ClientChatMessage))]
        public void OnPacketChatMessage(GenericEvent data)
        {
            if (MyConfig.Instance.Settings.MessageSentToDiscord.StartsWith("null")) return;

            try
            {
                Player player = ServerInstance.Instance.Handlers
                    .PlayerHandler.Players.GetPlayerByConnectionId((long)data.Data.OriginalMessage.SenderConnectionId);

                string name = player.Name;
                string message = ((ClientChatMessage)data.Data.DeserializedObject).message;

                if (debugMode)
                    Console.WriteLine($"<- Sending Message To Discord");

                string outMsg = MyConfig.Instance.Settings.MessageSentToDiscord.Replace("(%PlayerName%)", name).Replace("(%ChatMessage%)", message).Replace("(%CurrentDateTime%)", DateTime.Now.ToString())
                    ?? $"Game Server - {name}: {message} ";

                DiscordClient.SendMessageToMainChannel(outMsg);

                if (MyConfig.Instance.Settings.PrintDiscordChatToConsole)
                    Console.WriteLine(outMsg);
            }
            catch (Exception ex)
            {
                GetLogger.Error(ex.ToString());
            }
        }

        private Task SocketClient_MessageReceived(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            string outMsg = String.Empty;

            if (message != null)
            {
                // if the channel is the main channel
                if (message.Channel.Id == MyConfig.Instance.Settings.MainChannelID)
                {
                    // if the sender isn't this bot
                    if (message.Author.Id != MyConfig.Instance.Settings.BotClientID)
                    {
                        outMsg = MyConfig.Instance.Settings.MessageSentToGameServer
                            .Replace("(%DiscordChannelName%)", message.Channel.Name)
                            .Replace("(%DiscordUserName%)", message.Author.Username)
                            .Replace("(%ChatMessage%)", message.Content)
                            .Replace("(%CurrentDateTime%)", new DateTime().ToString())
                            ?? $"Discord - {message.Author.Username}: {message.Content}";

                        GetControllers.Chat.SendToAll(Game.Configuration.Config.Singleton.AllChatColor, outMsg);

                        if (debugMode)
                            Console.WriteLine($"-> Got Message From Discord");
                    }
                }
            }

            return Task.Run(() => Console.WriteLine(!MyConfig.Instance.Settings.PrintDiscordChatToConsole ? String.Empty : outMsg));
        }
    }
}