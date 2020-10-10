using Game.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.ClientServer.Classes;

namespace IRSE.GUI.ObjectManipulator.Wrappers
{
    public class PlayerWrapper : ManipulatorWrapper
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

        [Category("Wealth")]
        public long Credit
        {
            get { return Player.PersonalState.GetCredit(); }
            set { Player.PersonalState.SetCredit(Controllers, Player, value, Debt, "Server");}
        }

        [Category("Wealth")]
        public long Debt
        {
            get { return Player.PersonalState.GetDebt(); }
            set { Player.PersonalState.SetCredit(Controllers, Player, Credit, value, "Server"); }
        }

    }

}
