using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataserviceBet : IDataserviceBet
    {
        public static DateTime Now { get; }
        DateTime utcDate = DateTime.UtcNow;
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

            if(bet != null) return bet; 

            return null!; 
        }
        public BetDTO CreateBet(string playername, int gid, double? amount)
        {
            using var db = new CasinoDBContext();

            DataserviceGame dataServiceGame = new DataserviceGame();
            Game game = dataServiceGame.GetGame(gid)!;

            if(game.MaxBet >= amount && game.MinBet <= amount)
            {
                Bet newBet = new Bet()
                {
                    PlayerName = playername,
                    Gid = gid,
                    Amount = amount,
                    Date = utcDate
                };

                db.Bets?.Add(newBet);
                db.SaveChanges();
                return GetBetById(newBet.Bid);
            }
            else {

                return null!;
            }

           

        }

        public BetDTO UpdateBet(int bid, double amount)
        {
            using var db = new CasinoDBContext();
            var oldBet = db.Bets!.Where(x => x.Bid == bid).FirstOrDefault();

            if(oldBet != null)
            {
                oldBet.Amount = amount;
                oldBet.Date = utcDate;
                db.SaveChanges();
                return GetBetById(oldBet.Bid); ;
            }
            else
            {
                return null!;
            }

        }


        public bool DeleteBet(int bid)
        {
            using var db = new CasinoDBContext();


            var bet = GetBetById(bid);
            if (bet == null)
            {
                return false;
            }
            db.Bets?.Remove(GetBet(bid)!);
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
