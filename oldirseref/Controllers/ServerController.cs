/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */


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

		[HttpGet]
		public BaseResult SetMotd(string motd)
		{
			try
			{
				Config.Settings.OnJoinMotd = motd;
				return new BaseResult(false, "Message of the day set to: " + motd);
			}
			catch (Exception ex)
			{
				return new BaseResult(true, "SetMOTD() Exception: " + ex.ToString());
			}
		}
	}
}
