using Game.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRSE.Managers.ConsoleCommands
{
    internal class IRSECommands
    {
        [SvCommandMethod("---------------------------IRSE COMMANDS------------------------------", "SPLITTER", 4, new SvCommandMethod.ArgumentID[] { })]
        public static void splitter(object caller, List<string> parameters)
        {

        }

        [SvCommandMethod("opengui|loadgui|lg", "opens IRSE gui window", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_opengui(object caller, List<string> parameters)
        {

            if (!Program.GUIDisabled)
            {
                Program.SetupGUI();
            }
            else
                Console.WriteLine("GUI DISABLED");
            
        }


        [SvCommandMethod("---------------------------PLUGIN COMMANDS------------------------------", "SPLITTER", 4, new SvCommandMethod.ArgumentID[] { })]
        public static void splitter2(object caller, List<string> parameters)
        {

        }
    }
}