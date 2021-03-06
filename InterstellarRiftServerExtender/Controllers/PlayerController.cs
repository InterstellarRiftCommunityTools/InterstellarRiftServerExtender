﻿using IRSE.Managers;
using IRSE.API.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
                IEnumerable<Game.Server.Player> ieplayers = ServerInstance.Instance.Handlers.PlayerHandler.Players.AllPlayers();

                if (ieplayers.Count<Game.Server.Player>() == 0)
                    return new PlayerResult(false, "No Players", irPlayers);

                foreach (Game.Server.Player player in ieplayers)
                {
                    irPlayers.Add(new IRPlayer(player.Name, player.ConnectionId));
                }

                return new PlayerResult(false, "Received Players", irPlayers);
            }
            catch (Exception ex)
            {
                return new PlayerResult(true, ex.ToString(), new List<IRPlayer>());
            }
        }
    }
}