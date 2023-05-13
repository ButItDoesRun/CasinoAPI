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
        public Boolean UpdateBet(int bid, double amount);
        public Boolean CreateBet(int bid, string playername, int gid, double amount, DateTime date);
        public bool DeleteBet(int bid);


    }
}
