using Game.Server;
using Game.Universe;
using IRSE.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRSE.GUI.ObjectManipulator
{
    public class ManipulatorWrapper
    {

        public static ControllerManager Controllers => ServerInstance.Instance.Handlers.ControllerManager;
        public static ServerGalaxy Galaxy => Controllers.Universe.Galaxy as ServerGalaxy;

        public ManipulatorWrapper()
        {

        }

    }
}
