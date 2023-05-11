using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataserviceMoneyPot : IDataserviceMoneyPot
    {
    //currently not used
        private IList<MoneyPot>? GetGamePots(int gid)
        {
            using var db = new CasinoDBContext();
            var pots = db.MoneyPots?.Where(x => x.Gid == gid).ToList();
            return pots;
        }

        public MoneyPotDTO? GetGamePot(int gid)
        {
            using var db = new CasinoDBContext();
            var pot = db.MoneyPots?
                .FromSqlInterpolated($"select * from get_gamepot({gid})")
                .Select(pot => new MoneyPotDTO
                {
                    Pid = pot.Pid,
                    Gid = pot.Gid,
                    Amount = pot.Amount
                })
                .FirstOrDefault();
            if (pot != null) return pot;
            return null;
        }

        public MoneyPotDTO? AddGamePot(int gid, double amount)
        {
            using var db = new CasinoDBContext();
            var newPot = db.MoneyPots?
                .FromSqlInterpolated($"select * from new_gamepot({gid}, {amount})")
                .Select(pot => new MoneyPotDTO
                {
                    Pid = pot.Pid,
                    Gid = pot.Gid,
                    Amount = pot.Amount
                })
                .FirstOrDefault();
            if (newPot != null) return newPot;
            return null;
        }

        public MoneyPotDTO? UpdateGamePot(int gid, double amount)
        {
            using var db = new CasinoDBContext();
            var updatedPot = db.MoneyPots?
                .FromSqlInterpolated($"select * from update_gamepot({gid}, {amount})")
                .Select(pot => new MoneyPotDTO
                {
                    Pid = pot.Pid,
                    Gid = pot.Gid,
                    Amount = pot.Amount
                })
                .FirstOrDefault();
            if (updatedPot != null) return updatedPot;
            return null;
        }

        public bool DeleteGamePot(int gid)
        {
            using var db = new CasinoDBContext();
            var game = GetGame(gid);
            if (game != null && game.Pid != null)
            {
                var pot = db.MoneyPots?.FirstOrDefault(x => x.Gid == gid && x.Pid == game.Pid);
                if (pot != null)
                {
                    db.MoneyPots?.Remove(pot);
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteGamePots(int gid)
        {
            using var db = new CasinoDBContext();
            var game = GetGame(gid);
            if (game != null)
            {
                try
                {
                    db.Database.ExecuteSqlInterpolated($"select delete_gamepots({gid})");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            return false;
        }

        private Game? GetGame(int gid)
        {
            using var db = new CasinoDBContext();
            var game = db.Games?.FirstOrDefault(x => x.Gid == gid);
            return game;
        }

    }
}
