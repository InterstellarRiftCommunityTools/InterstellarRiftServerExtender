using System;
using System.Reflection;

namespace IRSE.Managers.Events
{
    public class EventListener
    {
        private MethodInfo Function;
        private Type EventType;
        private Type MethodType;
        private bool IgnoreCanceled = false;

        public bool IgnoreCanceledEvent { get { return IgnoreCanceled; } }
        public Type GetEventType { get { return EventType; } }

        public EventListener(MethodInfo function, Type methodType, Type type)
        {
            Function = function;
            EventType = type;
            MethodType = methodType;
        }

        public void Execute(GenericEvent evnt)
        {
            //Todo Convert to Corret Type
            Function.Invoke(Activator.CreateInstance(MethodType), new Object[] { evnt });
        }
    }
}