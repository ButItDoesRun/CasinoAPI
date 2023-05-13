﻿using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class DataservicePlayer : IDataservicePlayer
    {

        //PLAYER METODS

        public PlayerDTO? GetPlayerByName(String name)
        {
            using var db = new CasinoDBContext();
            var player = db.Players?
                .Select(x => new PlayerDTO
                {
                    PlayerName = x.PlayerName,
                    Balance = x.Balance,
                    BirthDate = x.BirthDate,
                    Password = x.Password
                })
                .FirstOrDefault(x => x.PlayerName == name);

            return player;
        }

        public bool PlayerExists(string playername)
        {
            using var db = new CasinoDBContext();
            if (GetPlayerByName(playername) == null) return false;
            return true;
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



        public PlayerDTO? UpdatePlayerBalance(string playername, double amount)
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
        
    }
}
