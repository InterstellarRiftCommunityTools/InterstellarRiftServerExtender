using System;
using System.Diagnostics;
using System.Reflection;

namespace IRSE.ReflectionWrappers
{
    public abstract class ReflectionClassWrapper
    {
        #region Fields

        protected String m_namespace;
        protected Type m_classType;
        protected Assembly m_assembly;

        #endregion Fields

        #region Properties

        public abstract String ClassName { get; }
        public abstract String AssemblyName { get; }

        #endregion Properties

        #region Methods

        protected ReflectionClassWrapper(Assembly Assembly, String Namespace, String Class)
        {
            m_assembly = Assembly;
            m_namespace = Namespace;
            m_classType = Assembly.GetType(Namespace + "." + Class);
        }

        public virtual void Init()
        {
        }

        #endregion Methods
    }

    public class ReflectionMember
    {
        #region Fields

        protected Type m_classType;
        protected String m_signature;
        protected String m_className;
        protected MemberInfo[] m_members;

        #endregion Fields

        #region Properties

        public String ClassName { get { return m_className; } }
        public String Signature { get { return m_signature; } }

        #endregion Properties

        #region Methods

        protected ReflectionMember(String signature, String className, Type classType)
        {
            m_signature = signature;
            m_className = className;
            m_classType = classType;
            m_members = classType.GetMember(signature, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (m_members.Length == 0)
            {
                throw new ArgumentException(String.Format("Reflection Error: Signature {0} not found in {1}", signature, className));
            }
        }

        #endregion Methods
    }

    public class ReflectionField : ReflectionMember
    {
        #region Fields

        protected FieldInfo m_field;

        #endregion Fields

        #region Methods

        internal ReflectionField(String signature, String className, Type classType)
            : base(signature, className, classType)
        {
            m_field = classType.GetField(signature, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (m_field == null)
            {
                throw new ArgumentException(String.Format("Reflection Error: Field {0} not found in {1}", signature, className));
            }
        }

        public Object GetValue(Object obj)
        {
            return m_field.GetValue(obj);
        }

        public void SetValue(Object obj, Object value)
        {
            m_field.SetValue(obj, value);
        }

        #endregion Methods
    }

    public class ReflectionMethod : ReflectionMember
    {
        #region Methods

        private MethodInfo Get(Object[] parameters)
        {
            Type[] argTypes = new Type[parameters.Length];

            int i = 0;
            foreach (Object arg in parameters)
            {
                argTypes[i] = arg.GetType();
                i++;
            }

            return Get(argTypes);
        }

        private MethodInfo Get(Type[] paramTypes)
        {
            return m_classType.GetMethod(m_signature,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance,
                null,
                CallingConventions.Standard | CallingConventions.HasThis,
                paramTypes,
                null);
        }

        private MethodInfo Get(Type[] paramTypes, Type genericType)
        {
            MethodInfo[] methods = m_classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                if (method.Name == Signature)
                {
                    MethodInfo methodInfo = method.MakeGenericMethod(genericType);
                    if (method != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        int i = 0;
                        int paramcount = paramTypes.Length;
                        bool valid = true;
                        foreach (ParameterInfo parameter in parameters)
                        {
                            if (i == paramcount)
                            {
                                valid = false;
                                break;
                            }
                            if (parameter.ParameterType != paramTypes[i])
                            {
                                valid = false;
                                break;
                            }
                            i++;
                        }
                        if (valid)
                        {
                            return methodInfo;
                        }
                    }
                }
            }

            return null;
        }

        public Object Call(Object obj, Object[] parameters)
        {
            if (parameters == null)
            {
                parameters = new Object[] { };
            }

            MethodInfo methodInfo = Get(parameters);

            if (methodInfo == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(String.Format("Overloaded method not found for {0}.{1} with argument types: {2}. Stack Trace: {3}", ClassName, Signature, parameters.ToString(), (new StackTrace()).ToString()));
                return null;
            }

            return methodInfo.Invoke(obj, parameters);
        }

        public Object Call(Object obj, Object[] parameters, Type[] paramTypes)
        {
            if (parameters == null)
            {
                parameters = new Object[] { };
            }

            MethodInfo methodInfo = Get(paramTypes);

            if (methodInfo == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(String.Format("Overloaded method not found for {0}.{1} with argument types: {2}. Stack Trace: {3}", ClassName, Signature, parameters.ToString(), (new StackTrace()).ToString()));
                return null;
            }

            return methodInfo.Invoke(obj, parameters);
        }

        public Object Call(Object obj, Object[] parameters, Type[] paramTypes, Type genericType)
        {
            if (parameters == null)
            {
                parameters = new Object[] { };
            }

            MethodInfo methodInfo = Get(paramTypes, genericType);

            if (methodInfo == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(String.Format("Overloaded method not found for {0}.{1} with argument types: {2}. Stack Trace: {3}", ClassName, Signature, parameters.ToString(), (new StackTrace()).ToString()));
                return null;
            }

            return methodInfo.Invoke(obj, parameters);
        }

        internal ReflectionMethod(String signature, String className, Type classType)
            : base(signature, className, classType)
        {
        }

        #endregion Methods
    }
}