using Aluna;
using Game.ClientServer;
using Game.ClientServer.Classes;
using Game.Server;
using Game.Universe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRSE.Modules
{
    public class RepairInit
    {
        private NLog.Logger mainLog;
        private static RepairInit instance;

        private Game.Server.ControllerManager _controllers;
        private Object _server;
        public static RepairInit Singleton => instance;

        public RepairInit(Game.Server.ControllerManager controllers, object server)
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();
            instance = new RepairInit(controllers, server);

            _controllers = controllers;
            _server = server;
        }

        public void ApplyServerRepairs() 
        {
           
        }
    

        public void ApplyUniverseRepairs()
        {
            mainLog.Info("Spawning Ghost Client?...");

            SolarSystem system1 = _controllers.Universe.Galaxy.GetSystem("Vectron Syx");
            SolarSystem system2 = _controllers.Universe.Galaxy.GetSystem("Sentinel Prime");
            SolarSystem system3 = _controllers.Universe.Galaxy.GetSystem("Scaverion");
            SolarSystem system4 = _controllers.Universe.Galaxy.GetSystem("Alpha Ventura");

            int port = _controllers.Network.NetGeneric.GetPort();

            Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {port} -GhostSystemName {system1.Identifier}");
            Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {port} -GhostSystemName {system2.Identifier}");
            Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {port} -GhostSystemName {system3.Identifier}");
            Process.Start(@"F:\IRSE2020\InterstellarRiftServerExtender\InterstellarRiftServerExtender\bin\Debug\IRGhostClient.exe", $"-ghostclient -ip 127.0.0.1 -port {port} -GhostSystemName {system4.Identifier}");

        }

    }
}
