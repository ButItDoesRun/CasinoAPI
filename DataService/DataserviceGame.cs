using System.Collections.Immutable;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataLayer
{
    public class DataserviceGame : IDataserviceGame
    {
        public GameDTO? GetGameById(int gid)
        {
            using var db = new CasinoDBContext();
            var game = db.Games?
                .Select(x => new GameDTO
                {
                    Gid = x.Gid,
                    Name = x.Name,
                    MinBet = x.MinBet,
                    MaxBet = x.MaxBet,
                    Pid = x.Pid
                })
                .FirstOrDefault(x => x.Gid == gid);
            if (game != null) return game;
            return null;
        }

        public GameDTO? CreateGame(string name, double minbet, double maxbet, double? potamount)
        {
            using var db = new CasinoDBContext();
            var createdGame = db.Games?
                .FromSqlInterpolated($"select * from create_game({name}, {minbet}, {maxbet}, {potamount})")
                .Select(game => new GameDTO
                {
                    Name = game.Name,
                    Gid = game.Gid,
                    MinBet = game.MinBet,
                    MaxBet = game.MaxBet,
                    Pid = game.Pid
                })
                .FirstOrDefault();
            if (createdGame != null) return createdGame;
            return null;
        }

        public GameDTO? CreateGame(string name, double minbet, double maxbet)
        {
            return CreateGame(name, minbet, maxbet, null);
        }

        public bool DeleteGame(int gid)
        {
            using var db = new CasinoDBContext();
            var game = GetGame(gid);
            if (game != null)
            {
                db.Games?.Remove(game);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public GameDTO? UpdateGame(int gid, string name, double minbet, double maxbet)
        {
            using var db = new CasinoDBContext();
            var updatedGame = db.Games?
                .FromSqlInterpolated($"select * from update_game({gid}, {name}, {minbet}, {maxbet})")
                .Select(game => new GameDTO
                {
                    Name = game.Name,
                    Gid = game.Gid,
                    MinBet = game.MinBet,
                    MaxBet = game.MaxBet
                })
                .FirstOrDefault();
            if (updatedGame != null) return updatedGame;
            return null;
        }

        private Game? GetGame(int gid)
        {
            using var db = new CasinoDBContext();
            var game = db.Games?.FirstOrDefault(x => x.Gid == gid);
            return game;
        }

    }
}
