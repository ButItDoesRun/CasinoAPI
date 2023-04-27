using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataLayer.DatabaseModel;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;

namespace DataLayer
{
    public class Dataservice :IDataservice
    {

       /* public SpecificMoneyPot GetMoneyPotById(int pid)
        {
            using var db = new CasinoDBContext();
            var MoneyPot = db.MoneyPots
                .Select(x => new SpecificMoneyPot
                {
                    PID = x.Pid,
                    amount = x.Amount
                })
                .FirstOrDefault(x => x.PID == pid);

            return MoneyPot;
        }
       */
        public SpecificPlayer GetPlayerByName(String name)
        {
            using var db = new CasinoDBContext();
            var player = db.Players
                .Select(x => new SpecificPlayer
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance,
                    BirthDate = x.BirthDate,
                    Password = x.Password
                })
                .FirstOrDefault(x => x.PlayerName == name);

            return player;
        }

       

    }
}
