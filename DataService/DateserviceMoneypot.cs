using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceMoneypot : IDataserviceMoneyPot
    {
        public SpecificMoneyPot GetMoneyPotByPid(int pid)
        {
            using var db = new CasinoDBContext();
            var moneyPot = db.MoneyPots
                .Select(x => new SpecificMoneyPot
                {
                    Pid = x.Pid,
                    Amount = x.Amount,
                })
                .FirstOrDefault(x => x.Pid == pid);

            return moneyPot;
        }
    }
}
