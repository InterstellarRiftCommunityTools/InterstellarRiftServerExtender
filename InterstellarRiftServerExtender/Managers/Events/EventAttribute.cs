using System;

namespace IRSE.Managers.Events
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class IRSEEventAttribute : Attribute
    {
        /// <summary>
        /// The type of event, just start typing Client into a typeof to find all the event types.
        /// </summary>
        public Type EventType;
    }
}