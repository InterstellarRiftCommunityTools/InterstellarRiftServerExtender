using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRSE.Controllers;

namespace IRSE.ResultObjects
{
	public class ServerStatusResult : BaseResult
	{
		public string IRSEUptime { get; set; }

		public ServerStatusResult(bool error, string status, TimeSpan uptime)
			:base(error, status)
		{
			IRSEUptime = String.Format("{0}:{1}:{2}:{3}",uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);
		}


	}


}
