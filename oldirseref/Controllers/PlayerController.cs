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
	public class IRPlayer
	{
		public string Name { get; set; }
		public long ConnectionID { get; set; }

		public IRPlayer(string name, long connectionId)
		{
			Name = name;
			ConnectionID = connectionId;
		}
	}


	public class PlayerController : ApiController
	{

		[HttpGet]
		public PlayerResult GetPlayers()
		{
			List<IRPlayer> irPlayers = new List<IRPlayer>();

			try
			{
				IEnumerable<Game.Server.Player> ieplayers = ServerInstance.Instance.Handlers.PlayerHandler.Players.Everyone();

				if(ieplayers.Count<Game.Server.Player>() == 0)
					return new PlayerResult(false, "No Players", irPlayers);

				foreach(Game.Server.Player player in ieplayers )
				{
					irPlayers.Add(new IRPlayer(player.Name, player.ConnectionID));
				}

				return new PlayerResult(false, "Recieved Players", irPlayers);
			}
			catch (Exception ex)
			{
				return new PlayerResult(true, ex.ToString(), new List<IRPlayer>());		
			}
		}
	
	}
}
