using System;

namespace IRSE.Managers.Events
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class IRSEEventAttribute : Attribute
    {
        public Type EventType;
    }
}