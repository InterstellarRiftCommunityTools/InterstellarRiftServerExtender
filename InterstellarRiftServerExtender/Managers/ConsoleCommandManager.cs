using Game.Framework;
using Game.Server;
using IRSE.Managers.ConsoleCommands;
using IRSE.Managers.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IRSE.Managers
{
    internal class ConsoleCommandManager
    {

        public static CommandSystem IRCommandSystem { get; private set; }

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
                    commandSystem.AddCommand(new Command(svCommandMethod.Names, "IRSE: "+svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
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
                    commandSystem.AddCommand(new Command(svCommandMethod.Names, "P:"+Plugin.Name+": "+svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
        }

        public static void InitAndReplace(ControllerManager controllers)
        {
            IRCommandSystem = (CommandSystem)ServerInstance.Instance.Assembly.GetType("Game.Server.SvCommands")
                .GetField("m_commandSystem", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            IRCommandSystem.AddCommand(new Command("help", "Lists all console commands", (CommandArgument[])null, new EventHandler<List<string>>(i_outputHelp), 0, false, false), true);

            ConsoleCommandManager.InitExtendedIRCommands(IRCommandSystem, controllers);
            ConsoleCommandManager.InitIRSECommands(IRCommandSystem);
            
            IRCommandSystem.RemoveCommand("start");
        }

        private static void i_outputHelp(object caller, List<string> parameters)
        {
            List<Command> pluginCommandList = new List<Command>();
            List<Command> irseCommandList = new List<Command>();
            List<Command> commandList = new List<Command>();
            foreach (Command command in IRCommandSystem.Commands.Values)
            {
                if (!commandList.Contains(command) && IRCommandSystem.SecurityHandler(caller, command.RequiredRight))
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
                IRCommandSystem.OutputHandler(caller, string.Join(" | ", command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRCommandSystem.OutputHandler(caller, "     " + command.Description);
            }

            if(irseCommandList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                IRCommandSystem.OutputHandler(caller, "\n----------------------------------------------IRSE COMMANDS----------------------------------------------\n");
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
                IRCommandSystem.OutputHandler(caller, string.Join(" | ", (IEnumerable<string>)command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRCommandSystem.OutputHandler(caller, "     " + command.Description);
            }

            if (pluginCommandList.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                IRCommandSystem.OutputHandler(caller, "\n---------------------------------------------PLUGIN COMMANDS---------------------------------------------\n");
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
                IRCommandSystem.OutputHandler(caller, string.Join(" | ", (IEnumerable<string>)command.Names) + " " + str1 + ":");
                Console.ResetColor();
                IRCommandSystem.OutputHandler(caller, "     " + command.Description);
            }
        }
    }
}