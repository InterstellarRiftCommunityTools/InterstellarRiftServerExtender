using System;

namespace IRSE.API.ResultObjects
{
    public class ServerStatusResult : BaseResult
    {
        public string IRSEUptime { get; set; }

        public ServerStatusResult(bool error, string status, TimeSpan uptime)
            : base(error, status)
        {
            IRSEUptime = String.Format("{0}:{1}:{2}:{3}", uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);
        }
    }
}