using System;

namespace IRSE.Managers.Events
{
    public class Event
    {
        private Type Type;

        private Boolean canceled = false;

        public Type GetEventType { get { return Type; } }
        public Boolean IsCanceled { get { return canceled; } }

        public Event(Type type)
        {
            Type = type;
        }

        public virtual void PreRun()
        {
        }

        public virtual void Run()
        {
        }

        public virtual void PostRun()
        {
        }
    }
}