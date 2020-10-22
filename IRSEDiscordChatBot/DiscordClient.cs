using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IRSEDiscordChatBot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IRSEDiscordChatBot
{
    public class DiscordClient
    {
        public static DiscordSocketClient SocketClient;
        public static DiscordClient Instance;
        public static ServiceProvider Services;
        public static bool botStarted = false;
        private Thread serverThread;

        public DiscordClient()
        {
            try
            {
                Instance = this;

                Services = ConfigureServices();
                SocketClient = Services.GetRequiredService<DiscordSocketClient>();

                Console.WriteLine(!MyConfig.Instance.Settings.DebugMode ? String.Empty : "IRSEDiscordChatBot - Bot Client Constructed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error constructing DiscordPlugin:\n" + ex.ToString());
            }
        }

        public void Start()
        {
            serverThread = new Thread(ThreadStart);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void ThreadStart()
            => StartBotAsync().GetAwaiter().GetResult();

        public void StopBot()
        {
            SocketClient.StopAsync().GetAwaiter().GetResult();
            serverThread.Abort();
        }

        private async Task StartBotAsync()
        {
            try
            {
                if (!botStarted)
                {
                    SocketClient.Connected += _client_Connected;
                    SocketClient.Log += _client_Log;
                    SocketClient.LoggedIn += _client_LoggedIn;

                    Services.GetRequiredService<CommandService>().Log += _client_Log;

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

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();
        }

        private Task _client_LoggedIn()
        {
            return Task.Run(() => Console.WriteLine(!MyConfig.Instance.Settings.DebugMode ? String.Empty : "IRSEDiscordChatBot - Logged In"));
        }

        private Task _client_Connected()
        {
            botStarted = true;
            return Task.Run(() => Console.WriteLine(!MyConfig.Instance.Settings.DebugMode ? String.Empty : "IRSEDiscordChatBot - Connected"));
        }

        private Task _client_Log(LogMessage arg)
        {
            return Task.Run(() => Console.WriteLine(MyConfig.Instance.Settings.PrintDiscordLogToConsole ? arg.Message : String.Empty));
        }

        internal static async void SendMessageToChannel(ulong channelID, string message)
        {
            if (!MyConfig.Instance.Settings.Enabled || !botStarted)
                return;

            try
            {
                await (SocketClient.GetChannel(channelID) as IMessageChannel).SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IRSEDiscordChatBot - Channel ({channelID}) Exception: {ex.Message}");
            }
        }

        internal static async void SendMessageToMainChannel(string message)
        {
            if (!MyConfig.Instance.Settings.Enabled || !botStarted)
                return;

            try
            {
                await (SocketClient.GetChannel(MyConfig.Instance.Settings.MainChannelID) as IMessageChannel).SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("IRSEDiscordChatBot - Main Channel Error: " + ex.Message);
            }
        }
    }
}