using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRSEDiscordChatBot
{
    public class DiscordClient
    {
        public static DiscordSocketClient SocketClient = new DiscordSocketClient();
        public static DiscordClient Instance;

        private static bool debugMode;

        public static bool botStarted = false;

        public DiscordClient(MyConfig config)
        {
            try
            {
                Instance = this;

                Console.WriteLine(!config.Settings.DebugMode ? String.Empty : "IRSEDiscordChatBot - Bot Client Constructed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error constructing DiscordPlugin:\n" + ex.ToString());
            }

        }

        public void Start()
        {
            var serverThread = new Thread(ThreadStart);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void ThreadStart()
            => StartBotAsync().GetAwaiter().GetResult();

        private async Task StartBotAsync()
        {
            try
            {
                if (!botStarted)
                {
                    Console.WriteLine("IRSEDiscordChatBot - Bot Connecting");

                    SocketClient.Connected += _client_Connected;
                    SocketClient.Log += _client_Log;
                    SocketClient.LoggedIn += _client_LoggedIn;

                    try
                    {
                        await SocketClient.LoginAsync(TokenType.Bot, MyConfig.Instance.Settings.DiscordToken);
                    }
                    catch { }
                    try
                    {
                        await SocketClient.StartAsync();
                    }
                    catch { }
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSEDiscordChatBot - Start Exception: " + ex.ToString());
            }
        }

        private Task _client_LoggedIn()
        {
            return Task.Run(() => Console.WriteLine(!debugMode ? String.Empty : "IRSEDiscordChatBot - Logged In"));
        }

        private Task _client_Connected()
        {
            botStarted = true;
            return Task.Run(() => Console.WriteLine(!debugMode ? String.Empty : "IRSEDiscordChatBot - Connected"));
        }

        private Task _client_Log(LogMessage arg)
        {
            return Task.Run(() => Console.WriteLine(MyConfig.Instance.Settings.PrintDiscordLogToConsole ? arg.Message : String.Empty));
        }

        public static void SendMessageToChannel(ulong channelID, string message)
        {
            (SocketClient.GetChannel(channelID) as IMessageChannel).SendMessageAsync(message);
        }

        internal void SendMessageToMainChannel(string message)
        {
            (SocketClient.GetChannel(MyConfig.Instance.Settings.MainChannelID) as IMessageChannel).SendMessageAsync(message);
        }
    }
}
