﻿using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceBets : IDataserviceBets
    {

        public IList<BetListElement> GetBets(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets
                .Include(x => x.Game)
                .Select(x => new BetListElement
                {
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    GameName = x.Game.Name,
                    Amount = x.Amount,
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (bets == null) return null;

            return bets;
        }


        //Helper functions
        public int GetNumberOfBets()
        {
            using var db = new CasinoDBContext();

            return db.Bets
                .Select(x => new BetListElement { }).Count();
        }
    }
}