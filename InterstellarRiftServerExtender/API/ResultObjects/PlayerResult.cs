using IRSE.Controllers;
using System.Collections.Generic;

namespace IRSE.API.ResultObjects
{
    public class PlayerResult : BaseResult
    {
        public List<IRPlayer> Players { get; set; }
        public int PlayerCount { get { return Players.Count; } }

        public PlayerResult(bool error, string status, List<IRPlayer> players)
                         : base(error, status)
        {
            Players = players;
        }
    }
}