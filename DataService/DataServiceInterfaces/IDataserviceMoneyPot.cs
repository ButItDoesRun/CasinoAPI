using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataserviceMoneyPot
    {
        public MoneyPotDTO? GetGamePot(int gid);

        public MoneyPotDTO? AddGamePot(int gid, double amount);

        public MoneyPotDTO? UpdateGamePot(int gid, double amount);

        public bool DeleteGamePot(int gid);

        public bool DeleteGamePots(int gid);

    }
}
