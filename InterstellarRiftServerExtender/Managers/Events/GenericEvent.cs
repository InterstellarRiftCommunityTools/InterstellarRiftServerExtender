using Game.Framework.Networking;
using System;

namespace IRSE.Managers.Events
{
    public class GenericEvent : Event
    {
        public Type EID;
        public RPCData Data;

        public GenericEvent(Type type, RPCData data) : base(type)
        {
            Data = data;
            EID = type;
        }
    }
}