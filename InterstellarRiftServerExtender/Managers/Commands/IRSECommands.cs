using Game.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRSE.Managers.ConsoleCommands
{
    internal class IRSECommands
    {
        
        [SvCommandMethod("---------------------------IRSE COMMANDS------------------------------", "", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void splitter(object caller, List<string> parameters)
        {

        }

        [SvCommandMethod("opengui", "If closed, will open and/or focus the GUI to the front.", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_openGui(object caller, List<string> parameters)
        {
            Program.SetupGUI();
        }

        [SvCommandMethod("forceupdate", "Forces an Update of IRSE with no prompts.", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_forceUpdate(object caller, List<string> parameters)
        {
            UpdateManager.Instance.CheckForUpdates(true).GetAwaiter().GetResult();

        }

        [SvCommandMethod("checkupdate", "Checks for IRSE updates. Prompts user with new update details.", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_checkUpdate(object caller, List<string> parameters)
        {
            UpdateManager.Instance.CheckForUpdates().GetAwaiter().GetResult();

        }

        [SvCommandMethod("irserestart", "Restarts IRSE, if autostart is set the server will start automatically.", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_restart(object caller, List<string> parameters)
        {
            UpdateManager.Instance.CheckForUpdates().GetAwaiter().GetResult();

        }

        [SvCommandMethod("stop", "Stops the server if its running!", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_stop(object caller, List<string> parameters)
        {
            if (ServerInstance.Instance.IsRunning)
                ServerInstance.Instance.Stop();
            else
                Console.WriteLine("The server is not running");
       
        }


        [SvCommandMethod("---------------------------PLUGIN COMMANDS------------------------------", "", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void splitter2(object caller, List<string> parameters)
        {

        }
        
    }
}