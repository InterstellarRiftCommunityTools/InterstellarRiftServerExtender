using Game.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.ClientServer.Classes;

namespace IRSE.EntityWrappers
{
    public class PlayerWrapper
    {
        [Browsable(false)]
        public readonly Player Player;


        public PlayerWrapper(Player player)
        {
            Player = player;

        }

        [Category("General")]
        [ReadOnly(true)]
        public string Name
        {
            get { return Player.Name; }
        }

        [Category("General")]
        [ReadOnly(true)]
        public ulong ID
        {
            get { return Player.ID; }
        }


    }


}
