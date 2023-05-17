using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataserviceBet
    {
        public BetDTO GetBetById(int bid);
        public BetDTO UpdateBet(int bid, double amount);
        public BetDTO CreateBet(string playername, int gid, double? amount);
        public bool DeleteBet(int bid);


    }
}
