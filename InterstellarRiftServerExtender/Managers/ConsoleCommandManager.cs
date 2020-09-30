using Game.Server;
using System.Collections.Generic;
using System.Reflection;
using IRSE.Managers.ConsoleCommands;
using System;
using Game.Framework;
using System.Linq;

namespace IRSE.Managers
{
    internal class ConsoleCommandManager
    {

        public static void InitCommands(ControllerManager controllers)
        {
            if (controllers != null) SvCommandMethod.UpdateControllers(controllers);

            List<MethodInfo> methods = new List<MethodInfo>();
            methods.AddRange(typeof(ExtendedCommands).GetMethods(BindingFlags.Static | BindingFlags.Public));
            methods.AddRange(typeof(IRSECommands).GetMethods(BindingFlags.Static | BindingFlags.Public));

            foreach (MethodInfo method in methods)
            {
                object[] customAttributes1 = method.GetCustomAttributes(typeof(SvCommandMethod), false);
                if (((IEnumerable<object>)customAttributes1).Count<object>() != 0)
                {
                    object[] customAttributes2 = null;
                    SvCommandMethod svCommandMethod = customAttributes1[0] as SvCommandMethod;
                    EventHandler<List<string>> handler = (EventHandler<List<string>>)Delegate.CreateDelegate(typeof(EventHandler<List<string>>), method);
                    CommandSystem.Singleton.AddCommand(new Command(svCommandMethod.Names, svCommandMethod.Description, svCommandMethod.Arguments, handler, svCommandMethod.RequiredRight, customAttributes2 != null && ((IEnumerable<object>)customAttributes2).Any<object>(), method.GetCustomAttribute<TalkCommandAttribute>() != null), true);
                }
            }
            //CommandSystem.Singleton.SortCommands();
        }

    }
}