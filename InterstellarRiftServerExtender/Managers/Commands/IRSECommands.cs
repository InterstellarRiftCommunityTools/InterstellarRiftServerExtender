using Game.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRSE.Managers.ConsoleCommands
{
    internal class IRSECommands
    {

        [SvCommandMethod("opengui|loadgui|lg", "opens IRSE gui window", 3, new SvCommandMethod.ArgumentID[] { })]
        public static void c_opengui(object caller, List<string> parameters)
        {
            Program.LoadGUI();
        }


    }
}