using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using Game.Configuration;
using Game.Framework;

namespace IRSE.Modules.GameConfig
{ 
    public class ServerConfigConverter
    {
        #region Fields


        private static ServerConfigConverter m_instance;

        public static ServerConfigConverter Instance => m_instance == null ? m_instance = new ServerConfigConverter() : m_instance;

        #endregion Fields

        public ServerConfigConverter()
        {

        }

        #region Methods

 

        public bool BuildAndUpdateConfigProperties() 
        {
            return false;
            // get all the public properties from the class
            ServerConfig serverConfig = new ServerConfig();

            FieldInfo[] fieldInfos = serverConfig.GetType().GetFields();


            string scriptTop = 
                "// This file was generated with ServerConfigConverter class\n" +
                "// To allow a PropertyGrid to use IRs fields as properties.\n" +
                "\n"+
                "using System.ComponentModel;\n" +
                "using Game.Configuration;\n" +
                "\n"+
                "namespace IRSE.Modules.GameConfig\n" +
                "{ \n" +
                "   public class ServerConfigProperties\n" +
                "   {\n"+
                "       \n" +
                "        private static ServerConfigProperties m_instance;"+
                "        public static ServerConfigProperties Instance => m_instance == null ? m_instance = new ServerConfigProperties() : m_instance;"+
                "       \n"+
                "        public ServerConfigProperties()\n" +
                "        {\n" +
                "        \n"+
                "        }\n";




            string code = "";
            foreach (FieldInfo field in fieldInfos) {


                if (field == null)
                    continue;
                
                ConfigOptionAttribute attribute = field.GetCustomAttribute<ConfigOptionAttribute>();
                if (attribute == null)
                    continue;

                string name = field.Name;
                string category = attribute.GetCategoryName();
                string description = Game.Configuration.Localization.Singleton.GetString("ServerConfigOptions", attribute.Description);
                string type = field.FieldType.ToString();

                dynamic value = field.GetValue(serverConfig);


                if (type.Contains("Dictionary") || type.Contains("List"))
                    continue;
              
                code += 
                    "\n" +
                    $"      [Category(\"{category}\")]\n" +
                    $"      [Description(\"{description}\")]\n" +
                    $"      public {type} {name} \n" +
                    $"      {{get{{return ServerConfig.Singleton.{name};}} set {{ServerConfig.Singleton.{name} = value;}}}}\n"+
                    "\n";
            }

            string scriptBottom =
                "  }\n" +
                "}\n"+
                "\n";
            System.IO.File.WriteAllText(@"F:\Wrex\Desktop\New folder\WriteText.cs", scriptTop + code + scriptBottom);

            return false;
        }






        private static dynamic GetDynamicObject(Dictionary<string, object> properties)
        {
            return new DynamicGameConfig(properties);
        }

        public sealed class DynamicGameConfig : DynamicObject
        {
            private readonly Dictionary<string, object> _properties;

            public DynamicGameConfig(Dictionary<string, object> properties)
            {
                _properties = properties;
                
            }

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return _properties.Keys;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                if (_properties.ContainsKey(binder.Name))
                {
                    result = _properties[binder.Name];
                    return true;
                }
                else
                {
                    result = null;
                    return false;
                }
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                if (_properties.ContainsKey(binder.Name))
                {
                    _properties[binder.Name] = value;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        #endregion Methods
    }
}
