
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using IRSE.ResultObjects;
using IRSE.Managers;

namespace IRSE.Controllers
{


	public class ServerController : ApiController
	{

		[HttpGet]
		public ServerStatusResult GetStatus()
		{
			try
			{
				return new ServerStatusResult(false, "Hai Ettna! Here are some juicy status updates for you!", ServerInstance.Instance.Uptime);
			}
			catch (Exception ex)
			{
				return new ServerStatusResult(true, "GetStatus() Exception:" + ex.ToString(), new TimeSpan());
			}
		}

	}
}
