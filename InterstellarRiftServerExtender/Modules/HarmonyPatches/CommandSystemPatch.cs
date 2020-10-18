using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Configuration;
using Game.Framework;
using Game.Server;
using HarmonyLib;
using IRSE.Managers;

namespace IRSE.Modules.HarmonyPatches
{
    /// <summary>
    /// This class is supposed to help 7th cores script enter commands.
    /// </summary>
    [HarmonyPatch(typeof(CommandSystem), "RemoveAmbiguityStep")]
    internal static class RemoveAmbiguityStep
    {
        private static void Prefix(object sender, CommandArgument[] args, List<string> newValues, int i, CommandSystem.AmbiguityResultDelegate resultCallback)
        {
            //if (Config.Instance.Settings.DisableServerCommandAmbiguity)
            //{
            //}
            FileLog.Log($"Sender: {sender} Args: [{args.Join()}] Values: [{newValues.Join()}] I: {i}");
        }
    }

    /// <summary>
    /// This class is stopping the internal command hooks from creating handlers as i handle that to insert my own commands.
    /// </summary>
    [HarmonyPatch(typeof(SvCommands), "InitCommandHooks")]
    internal static class StopInternalServerCommandHooks
    {
        /*
        public static void InitCommandHooks()
        {
          SvCommands.m_commandSystem.SecurityHandler = new Func<object, int, bool>(SvCommands.i_securityHandler);
          SvCommands.m_commandSystem.OutputHandler += new EventHandler<string>(SvCommands.i_outputHandler);
          SvCommands.m_commandSystem.ErrorHandler += new CommandSystem.ErrorHandlerDelegate(SvCommands.i_errorHandler);
          SvCommands.m_commandSystem.InputHandler += new CommandSystem.InputDelegate(SvCommands.i_inputHandler);
          SvCommands.m_commandSystem.ExecuteHandler += new CommandSystem.ExecuteHandlerDelegate(SvCommands.i_executeHandler);
        }
         */

        /// <summary>
        /// Disables SvCommands class hooks
        /// </summary>
        /// <returns></returns>
        private static bool Prefix()
        {
            return false;
        }
    }
}