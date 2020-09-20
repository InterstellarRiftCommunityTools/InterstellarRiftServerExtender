
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRSE.Controllers;

namespace IRSE.ResultObjects
{
	public class PlayerResult
	{
		public bool Error { get; set; }
		public string Status { get; set; }
		public List<IRPlayer> Players { get; set;}
		public int PlayerCount { get { return Players.Count; } }

		public PlayerResult(bool error, string status, List<IRPlayer> players)
		{
			Error = error;
			Status = status;
			Players = players;
		}
	}


}
