using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer
{

    public class DataservicePlayer : IDataservicePlayer
    {

        //PLAYER METODS

        public PlayerDTO? GetPlayerByID(String name)
        {
            using var db = new CasinoDBContext();
            var player = db.Players?
                .Select(x => new PlayerDTO
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance,
                    BirthDate = x.BirthDate,
                    Password = x.Password,
                    IsDeveloper = x.IsDeveloper
                })
                .FirstOrDefault(x => x.PlayerName == name);

            if (player != null)
            return player;
            else return null;
        }

        public Player? GetPlayerObject(String name)
        {
            using var db = new CasinoDBContext();
            var player = db.Players?
                .Select(x => new Player
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance,
                    BirthDate = x.BirthDate,
                    Password = x.Password,
                    IsDeveloper = x.IsDeveloper
                })
                .FirstOrDefault(x => x.PlayerName == name);

            return player;
        } 


        public double? GetPlayerBalance(String playername)
        {
            using var db = new CasinoDBContext();
            var playerBalance = db.Players?
                .Select(x => new 
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance
                })
                .FirstOrDefault(x => x.PlayerName == playername);

            var balance = playerBalance!.Balance;

            return balance;
        }


        public bool PlayerExists(string playername)
        {
            using var db = new CasinoDBContext();
            var player = db.Players?.FirstOrDefault(x => x.PlayerName == playername);
            if (player == null) return false;
            return true;
        }

        public Salt GetPlayerSalt(string playername)
        {
            using var db = new CasinoDBContext();
            var salt = db.Salts?.FirstOrDefault(x => x.PlayerName == playername);
            if (salt == null) return null!;
            return salt;
        }

        public bool CreatePlayer(string playername, string password, DateOnly birthdate, double balance, string ssalt)
        {
            using var db = new CasinoDBContext();

            var player = new Player
            {
                PlayerName = playername,
                BirthDate = birthdate,
                Password = password,
                Balance = balance

            };

            var salt = new Salt
            {
                PlayerName = playername,
                SSalt = ssalt,

            };

            db.Players?.Add(player);
            db.Salts?.Add(salt);
            db.SaveChanges();

            if (PlayerExists(playername)) return true;
            return false;
        }

        public bool MakeDeveloper(string playername)
        {
            using var db = new CasinoDBContext();
            var player = GetPlayerObject(playername);        
            player!.IsDeveloper = true;           

            db.Players?.Update(player);     
            db.SaveChanges();

            var update = GetPlayerObject(playername);

            if (update!.IsDeveloper) return true;
            return false;
        }



        public PlayerDTO? UpdatePlayerBalance(string playername, double? amount)
        {
            using var db = new CasinoDBContext();
            var updatedBalance = db.Players?
                .FromSqlInterpolated($"select * from update_balance({playername}, {amount})")
                .Select(player => new PlayerDTO
                {
                    PlayerName = player.PlayerName,
                    Balance = player.Balance
                })
                .FirstOrDefault();
            if (updatedBalance != null) return updatedBalance;
            return null;
        }

        public bool AddWinOrLossToPlayerBalance(string playername, double? amount)
        {
            using var db = new CasinoDBContext();

            var currentBalance = GetPlayerBalance(playername);
            Console.WriteLine(currentBalance);

            var newBalance = currentBalance + amount;

            PlayerDTO? playerBalance = UpdatePlayerBalance(playername, newBalance);

            if (playerBalance!.Balance != newBalance) return false;

            return true;
        }



        public bool DeletePlayer(string playername)
        {
            using var db = new CasinoDBContext();
            var player = db.Players?.FirstOrDefault(x => x.PlayerName == playername);
            if (player != null)
            {
                try
                {

                    db.Players?.Remove(player);
                    db.SaveChanges();
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



        public PlayerDTO UpdatePlayer(string playername, string newHash, string newSalt, DateOnly newBirthdate)
        {
            using var db = new CasinoDBContext();
            var updatedPlayer = db.Players?
                .FromSqlInterpolated($"select * from update_player({playername}, {newHash}, {newSalt}, {newBirthdate})")
                .Select(player => new PlayerDTO
                {
                    PlayerName = player.PlayerName,
                    Password = player.Password,
                    BirthDate = newBirthdate,
                    Balance = player.Balance
                })
                .FirstOrDefault();
            if (updatedPlayer != null) return updatedPlayer;
            return null!;
        }







        //PLAYER LIST METHODS

        public IList<PlayersDTO>? GetPlayers(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var players = db.Players?
                .Select(x => new PlayersDTO
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (players == null) return null;

            return players;
        }



        //Helper functions


        public int GetNumberOfPlayers()
        {
            using var db = new CasinoDBContext();
            var result = db.Players?
                .Select(x => new PlayersDTO { }).Count();
            if (result == null) return 0;
            return (int)result;

        }


        //GAME LIST               
        
        public IList<GamesDTO>? GetPlayerGames(string playername)
        {
            using var db = new CasinoDBContext();

            var playerGames = db.Bets?
                .Include(x => x.Game)
                .ThenInclude(x => x!.MoneyPot)
                .Where(x => x.PlayerName!.Equals(playername))
                .Select(x => new GamesDTO {
                    Gid = x.Gid,
                    Name = x.Game!.Name,
                    MinBet = x.Game.MinBet,
                    MaxBet = x.Game.MaxBet,
                    Pid = x.Game.Pid,
                    PotAmount = x.Game!.MoneyPot!.Amount,
                })
                .GroupBy(x => x.Gid).Select(y => y.First()).Distinct()
                .ToList();

            if (playerGames == null) return null;

            return playerGames;
        }

        

        

    }
}
