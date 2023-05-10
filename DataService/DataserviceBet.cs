using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceBet : IDataserviceBet
    {
        public BetDTO GetBetById(int bid)
        {
            using var db = new CasinoDBContext();
            var bet = db.Bets
                .Select(x => new BetDTO
                {
                    Bid = x.Bid,
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    Amount = x.Amount,
                    Date = x.Date
                })
                .FirstOrDefault(x => x.Bid == bid);

            return bet;
        }

        public BetDTO CreateBet(int bid, string playername, int gid, double amount, DateTime date)
        {
            using var db = new CasinoDBContext();

            Bet newBet = new Bet()
            {
                Bid = bid,
                PlayerName = playername,
                Gid = gid,
                Amount = amount,
                Date = date
            };

            db.Bets.Add(newBet);
            db.SaveChanges();

            return GetBetById(bid);

        }

        public bool DeleteBet(int bid)
        {
            using var db = new CasinoDBContext();


            var bet = GetBetById(bid);
            if (bet == null)
            {
                return false;
            }
            db.Bets.Remove(GetBet(bid));
            db.SaveChanges();
            return true;
        }

        private Bet? GetBet(int bid)
        {
            using var db = new CasinoDBContext();
            var bet = db.Bets?.FirstOrDefault(x => x.Bid == bid);
            return bet;
        }
    }
}
