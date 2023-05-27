using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using DataLayer.DatabaseModel.CasinoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


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

            using var transaction = db.Database.BeginTransaction();

            DataserviceGame dataServiceGame = new DataserviceGame();
            Game game = dataServiceGame.GetGame(gid)!;

            DataservicePlayer dataservicePlayer = new DataservicePlayer();

            try
            {
                var player = db.Players!.Where(x => x.PlayerName == playername).FirstOrDefault();

                if ((game.MaxBet >= amount && game.MinBet <= amount) && amount < player.Balance)
                {
                    Bet newBet = new Bet()
                    {
                        PlayerName = playername,
                        Gid = gid,
                        Amount = amount,
                        Date = utcDate
                    };

                    player.Balance = player.Balance - amount;
                    db.SaveChanges();

                    db.Bets?.Add(newBet);
                    db.SaveChanges();

                    transaction.Commit();

                    return GetBetById(newBet.Bid);

                }
                else return null!;
            }
            catch (Exception ex) { return null!; }
            


        }

        public BetDTO UpdateBet(int bid, double amount)
        {
            using var db = new CasinoDBContext();

            using var transaction = db.Database.BeginTransaction();

            try
            {
                var oldBet = db.Bets!.Where(x => x.Bid == bid).FirstOrDefault();

                if (oldBet == null) return null!;

                DataserviceGame dataServiceGame = new DataserviceGame();
                Game game = dataServiceGame.GetGame(oldBet.Gid)!;

                if (oldBet.PlayerName == null) return null!;

                DataservicePlayer dataservicePlayer = new DataservicePlayer();
                var player = db.Players!.Where(x => x.PlayerName == oldBet.PlayerName).FirstOrDefault();

                if (oldBet != null)
                {
                    if ((game.MaxBet >= amount && game.MinBet <= amount) && amount < player.Balance)
                    {
                        oldBet.Amount = amount;
                        oldBet.Date = utcDate;
                        player.Balance = player.Balance - amount;
                        db.SaveChanges();

                        transaction.Commit();

                        return GetBetById(oldBet.Bid); ;
                    }
                    else return null;
                }
                else return null!;
            }
            catch (Exception ex) { return null!; }


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
