using Game.Framework;
using Game.Server;
using IRSE.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Panel = System.Windows.Forms.Panel;
using Game.Framework.ArgumentValidators;
using BaseValidator = Game.Framework.ArgumentValidators.BaseValidator;
using System.Windows.Controls;

namespace IRSE.GUI.Forms
{
    public class CommandVisualizer
    {
        public Dictionary<string, List<Type>> Map;

        public CommandVisualizer()
        {
            Map = new Dictionary<string, List<Type>>();
        }

        

        public void BuildLayout(Panel panel = null)
        {
            MethodInfo[] commandMethods = ServerInstance.Instance.Assembly.GetType("Game.Server.SvCommands").GetMethods(BindingFlags.Public | BindingFlags.Static);

            foreach (MethodInfo method in commandMethods) {

                SvCommandMethod commandMethod = method.GetCustomAttribute<SvCommandMethod>();

                if(commandMethod != null)
                {
                    CommandArgument[] arguments = commandMethod.Arguments;

                    if(arguments != null && arguments.Length != 0)
                    {
                        foreach (var argument in arguments)
                        {
                            // Do something
                        }
                    }

                    // remove the c_ from the method name.
                    string name = method.Name.Replace("c_", "");
                    //Capitalize first letter
                    name = char.ToUpper(name[0]) + name.Substring(1);
                    //put spaces between capital letters
                    name = string.Concat(name.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

                    string description = commandMethod.Description;

                    //Map.Add(name+" : " + , new List<Type>() { });
                }
            
            }

        }

        public enum ControlType
        {
            TextBox,
            BoolInput
        }

        

        /*
        private static readonly Dictionary<SvCommandMethod.ArgumentID, List<Type>> m_commandsMap = new Dictionary<SvCommandMethod.ArgumentID, List<Type>>()
        {
          {
            SvCommandMethod.ArgumentID.message,
            new List<Type>()
            {
                typeof(string)
            }
          },
          {
            SvCommandMethod.ArgumentID.text,
            typeof()
          },
          {
            SvCommandMethod.ArgumentID.title,
            new CommandArgument(20, "title", false)
          },
          {
            SvCommandMethod.ArgumentID.player,
            (CommandArgument) new CommandArgument_Player(1, "player id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.playerName,
            new CommandArgument(3, "player name", false)
          },
          {
            SvCommandMethod.ArgumentID.optionalPlayer,
            (CommandArgument) new CommandArgument_Player(2, "player id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.ship,
            (CommandArgument) new CommandArgument_Ship(4, "ship id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.optionalShip,
            (CommandArgument) new CommandArgument_Ship(5, "ship id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.system,
            (CommandArgument) new CommandArgument_System(6, "system id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.optionalSystem,
            (CommandArgument) new CommandArgument_System(17, "system id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.optionalFleet,
            (CommandArgument) new CommandArgument_Fleet(16, "fleet id/name", false, (ControllerManager) null)
          },
          {
            SvCommandMethod.ArgumentID.level,
            new CommandArgument(7, "level", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.index,
            new CommandArgument(21, "index", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.password,
            new CommandArgument(8, "password", true, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.id,
            new CommandArgument(9, "id", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.verbosity,
            new CommandArgument(10, "verbosity", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.faction,
            new CommandArgument(11, "faction", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.amount,
            new CommandArgument(12, "amount", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.optionalAmount,
            new CommandArgument(13, "amount", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.resource,
            new CommandArgument(14, "resource", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.tool,
            new CommandArgument(15, "tool", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.optionalID,
            new CommandArgument(18, "id", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.seconds,
            new CommandArgument(22, "seconds", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.mass,
            new CommandArgument(23, "mass", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.basePrice,
            new CommandArgument(24, "base price", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.priceDeviant,
            new CommandArgument(25, "price deviant", false, (BaseValidator) IntegerValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.oldName,
            new CommandArgument(26, "old name", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.newName,
            new CommandArgument(27, "new name", false, (BaseValidator) StringValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.decimalValue,
            new CommandArgument(28, "decimal value", false, (BaseValidator) FloatValidator.DefaultInstance)
          },
          {
            SvCommandMethod.ArgumentID.systemTier,
            new CommandArgument(29, "systemTier", false, (BaseValidator) IntegerValidator.DefaultInstance)
          }
        };
        */
    }

}
