using Game.Configuration;
using Game.Framework;
using Game.Server;
using IRSE.Managers.ConsoleCommands;
using IRSE.Managers.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IRSE.Managers
{
    internal class ConsoleCommandManager
    {
        private static readonly Logger log = (Logger)Logger.Get(MethodBase.GetCurrentMethod().DeclaringType.ToString());
        public static CommandSystem IRSECommandSystem { get; private set; }

        public static void InitExtendedIRCommands(CommandSystem commandSystem, ControllerManager controllers)
        {
            if (controllers != null) SvCommandMethod.UpdateControllers(controllers);

            List<MethodInfo> methods = new List<MethodInfo>();
            methods.AddRange(typeof(ExtendedCommands).GetMethods(BindingFlags.Static | BindingFlags.Public));

            foreach (MethodInfo method in methods)
            {
                object[] customAttributes1 = method.GetCustomAttributes(typeof(SvCommandMethod), false);
                if (((IEnumerable<object>)customAttributes1).Count<object>() != 0)
                {
                    object[] customAttributes2 = null;
                    SvCommandMethod svCommandMethod = customAttributes1[0] as SvCommandMethod;
                    EventHandler<List<string>> handler = (EventHandler<List<string>>)Delegate.CreateDelegate(typeof(EventHandler<List<string>>), method);

                    commandSystem.AddCommand(new Command(svCommandMethod.Names, svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
        }

        public static void InitIRSECommands(CommandSystem commandSystem)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            methods.AddRange(typeof(IRSECommands).GetMethods(BindingFlags.Static | BindingFlags.Public));

            foreach (MethodInfo method in methods)
            {
                object[] customAttributes1 = method.GetCustomAttributes(typeof(SvCommandMethod), false);
                if (((IEnumerable<object>)customAttributes1).Count<object>() != 0)
                {
                    object[] customAttributes2 = null;
                    SvCommandMethod svCommandMethod = customAttributes1[0] as SvCommandMethod;
                    EventHandler<List<string>> handler = (EventHandler<List<string>>)Delegate.CreateDelegate(typeof(EventHandler<List<string>>), method);
                    commandSystem.AddCommand(new Command(svCommandMethod.Names, "IRSE: " + svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
        }

        public static void InitPluginCommands(CommandSystem commandSystem, PluginInfo Plugin)
        {
            ///Permission.Admin

            foreach (MethodInfo method in Plugin.MainClassType.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                object[] customAttributes1 = method.GetCustomAttributes(typeof(SvCommandMethod), false);
                if (((IEnumerable<object>)customAttributes1).Count<object>() != 0)
                {
                    object[] customAttributes2 = null;
                    SvCommandMethod svCommandMethod = customAttributes1[0] as SvCommandMethod;
                    EventHandler<List<string>> handler = (EventHandler<List<string>>)Delegate.CreateDelegate(typeof(EventHandler<List<string>>), method);
                    commandSystem.AddCommand(new Command(svCommandMethod.Names, "P:" + Plugin.Name + ": " + svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
        }

        public static void InitAndReplace(ControllerManager controllers)
        {
            IRSECommandSystem = (CommandSystem)ServerInstance.Instance.Assembly.GetType("Game.Server.SvCommands")
                .GetField("m_commandSystem", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            IRSECommandSystem.AddCommand(new Command("help", "Lists all console commands", (CommandArgument[])null, new EventHandler<List<string>>(i_outputHelp), 0, false, false), true);

            InitExtendedIRCommands(IRSECommandSystem, controllers);
            InitIRSECommands(IRSECommandSystem);

            IRSECommandSystem.RemoveCommand("start");
        }

        #region Handlers

        public static void InitHandlers()
        {
            IRSECommandSystem = CommandSystem.Singleton;

            ConsoleCommandManager.InitIRSECommands(IRSECommandSystem);

            IRSECommandSystem.OutputHandler += new EventHandler<string>(i_outputHandler);
            IRSECommandSystem.SecurityHandler = new Func<object, int, bool>(i_securityHandler);
            IRSECommandSystem.ErrorHandler += new CommandSystem.ErrorHandlerDelegate(i_errorHandler);
            IRSECommandSystem.InputHandler += new CommandSystem.InputDelegate(i_inputHandler);
            IRSECommandSystem.ExecuteHandler += new CommandSystem.ExecuteHandlerDelegate(i_executeHandler);
        }

        private static bool i_securityHandler(object sender, int requiredRights)
        {
            if (!(sender is Player))
            {
                if ((requiredRights & SvCommandMethod.ChatOnly) == SvCommandMethod.ChatOnly)
                    return false;
                if (requiredRights == 4)
                    return false; // for now
                if (requiredRights == 5) // server side irse commands
                    return sender is ControllerManager;
                return true;
            }
            requiredRights = requiredRights << 1 >> 1;
            return ((Player)sender).serverRights >= (ServerRights)requiredRights;
        }

        private static void i_errorHandler(object caller, string command, string message)
        {
            i_outputHandler(caller, message);
            if (!(caller is ControllerManager) || command.Length == 0)
                return;
            log.Info("Attempted to execute the following command: " + command, "Admin Commands");
        }

        private static void i_executeHandler(object caller, string command)
        {
            if (!(caller is ControllerManager) || command.Length == 0)
                return;
            log.Info("Executed the following command: " + command, "Admin Commands");
        }

        private static void i_outputHandler(object caller, string message)
        {
            Console.WriteLine(message);
        }

        private static void i_inputHandler(object caller, CommandSystem.InputResultDelegate callback)
        {
            byte[] buf = new byte[256];
            Stream inputStream = Console.OpenStandardInput();
            inputStream.BeginRead(buf, 0, buf.Length, (AsyncCallback)(ar =>
            {
                inputStream.EndRead(ar);
                callback(Encoding.UTF8.GetString(buf));
            }), (object)null);
        }

        private static void i_outputHelp(object caller, List<string> parameters)
        {
            List<Command> pluginCommandList = new List<Command>();
            List<Command> irseCommandList = new List<Command>();
            List<Command> commandList = new List<Command>();
            foreach (Command command in IRSECommandSystem.Commands.Values)
            {
                if (!commandList.Contains(command) && IRSECommandSystem.SecurityHandler(caller, command.RequiredRight))
                {
                    if (command.Description.StartsWith("IRSE:"))
                        irseCommandList.Add(command);
                    else if (command.Description.StartsWith("P:"))
                        pluginCommandList.Add(command);
                    else
                        commandList.Add(command);
                }
            }

            commandList.Sort((x, y) => x.Names.First().CompareTo(y.Names.First()));

            irseCommandList.Sort((x, y) => x.Names.First().CompareTo(y.Names.First()));

            foreach (Command command in commandList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string str1 = "";
                if (command.Arguments != null)
                {
                    foreach (CommandArgument commandArgument in command.Arguments)
                    {
                        string description = commandArgument.Description;
                        string str2 = !commandArgument.GetIsOptional(caller) ? "[" + description + "]" : "(" + description + ")";
                        str1 = str1 + str2 + " ";
                    }
                }
                IRSECommandSystem.OutputHandler(caller, string.Join(" | ", command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRSECommandSystem.OutputHandler(caller, "     " + command.Description);
            }

            if (irseCommandList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                IRSECommandSystem.OutputHandler(caller, "\n----------------------------------------------IRSE COMMANDS----------------------------------------------\n");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            foreach (Command command in irseCommandList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string str1 = "";
                if (command.Arguments != null)
                {
                    foreach (CommandArgument commandArgument in command.Arguments)
                    {
                        string description = commandArgument.Description;
                        string str2 = !commandArgument.GetIsOptional(caller) ? "[" + description + "]" : "(" + description + ")";
                        str1 = str1 + str2 + " ";
                    }
                }
                IRSECommandSystem.OutputHandler(caller, string.Join(" | ", (IEnumerable<string>)command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRSECommandSystem.OutputHandler(caller, "     " + command.Description);
            }

            if (pluginCommandList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                IRSECommandSystem.OutputHandler(caller, "\n---------------------------------------------PLUGIN COMMANDS---------------------------------------------\n");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            foreach (Command command in pluginCommandList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string str1 = "";
                if (command.Arguments != null)
                {
                    foreach (CommandArgument commandArgument in command.Arguments)
                    {
                        string description = commandArgument.Description;
                        string str2 = !commandArgument.GetIsOptional(caller) ? "[" + description + "]" : "(" + description + ")";
                        str1 = str1 + str2 + " ";
                    }
                }
                IRSECommandSystem.OutputHandler(caller, string.Join(" | ", (IEnumerable<string>)command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRSECommandSystem.OutputHandler(caller, "     " + command.Description);
            }
        }

        #endregion Handlers
    }
}