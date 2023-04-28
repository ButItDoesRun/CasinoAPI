using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataLayer
{
    public class DataserviceGame : IDataserviceGame
    {
        public SpecificGame? GetGameById(int gid)
        {
            using var db = new CasinoDBContext();
            var game = db.Games
                .Select(x => new SpecificGame
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

        public SpecificGame? CreateGame(string name, double minbet, double maxbet, double? potamount)
        {
            using var db = new CasinoDBContext();
            var createdGame = db.GamesRecords.
                FromSqlInterpolated($"select * from create_game({name}, {minbet}, {maxbet}, {potamount})")
                .Select(game => new SpecificGame
                {
                    Name = game.Name,
                    Gid = game.Gid,
                    MinBet = game.MinBet,
                    MaxBet = game.MaxBet,
                    Pid = game.Pid,
                    PotAmount = game.Amount
                })
                .FirstOrDefault();
            if (createdGame != null) return createdGame;
            return null;
        }

        public SpecificGame? CreateGame(string name, double minbet, double maxbet)
        {
            using var db = new CasinoDBContext();
            var createdGame = db.GamesRecords.FromSqlInterpolated($"select * from create_game({name}, {minbet}, {maxbet})")
                .Select(game => new SpecificGame
                    {
                        Name = game.Name,
                        Gid = game.Gid,
                        MinBet = game.MinBet,
                        MaxBet = game.MaxBet,
                        Pid = game.Pid,
                        PotAmount = game.Amount
                    })
                .FirstOrDefault();
            if (createdGame != null) return createdGame;
            return null;
        }

        public bool DeleteGame(int gid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGame(int gid, string name, double minbet, double maxbet)
        {
            throw new NotImplementedException();
        }

        public SpecificMoneyPot? AddGamePot(int gid, double amount)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGamePot(int gid, double amount)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGamePot(int gid)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGamePots(int gid)
        {
            throw new NotImplementedException();
        }
    }
}
