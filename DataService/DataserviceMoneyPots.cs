using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceMoneyPots : IDataserviceMoneyPots
    {
        public IList<MoneyPotListElement> GetMoneyPots(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var moneyPots = db.MoneyPots
                .Include(x => x.Game)
                .Select(x => new MoneyPotListElement
                {
                    Pid = x.Pid,
                    Gid = x.Game.Gid,              
                    GameName = x.Game.Name,
                    Amount = x.Amount,
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (moneyPots == null) return null;

            return moneyPots;
        }


        //Helper functions
        public int GetNumberOfMoneyPots()
        {
            using var db = new CasinoDBContext();

            return db.MoneyPots
                .Select(x => new MoneyPotListElement { }).Count();
        }
    }
}
