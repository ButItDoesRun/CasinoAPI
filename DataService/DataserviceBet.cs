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
            var bet = db.Bets?
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
        public Boolean CreateBet(int bid, string playername, int gid, double amount, DateTime date)
        {
            using var db = new CasinoDBContext();

            DataserviceGame dataServiceGame = new DataserviceGame();
            Game game = dataServiceGame.GetGame(gid);

            if(game.MaxBet >= amount && game.MinBet <= amount)
            {
                Bet newBet = new Bet()
                {
                    Bid = bid,
                    PlayerName = playername,
                    Gid = gid,
                    Amount = amount,
                    Date = date
                };

                db.Bets?.Add(newBet);
                db.SaveChanges();

                return true;
            }
            else {
                return false;
            }

           

        }

        public Boolean UpdateBet(int bid, double amount)
        {
            using var db = new CasinoDBContext();
            var updatedBet = db.Bets?
                .Select(bet => new BetDTO
                {
                    Bid = bid,
                    PlayerName = bet.PlayerName,
                    Gid = bet.Gid,
                    Amount = amount,
                    Date = bet.Date
                })
                .FirstOrDefault();
            if (updatedBet != null) return true;
            return false;
        }


        public bool DeleteBet(int bid)
        {
            using var db = new CasinoDBContext();


            var bet = GetBetById(bid);
            if (bet == null)
            {
                return false;
            }
            db.Bets?.Remove(GetBet(bid));
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
