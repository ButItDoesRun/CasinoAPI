using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceBets : IDataserviceBets
    {

        public IList<BetsDTO> GetBets(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets?
                .Include(x => x.Game)
                .Select(x => new BetsDTO
                {
                    Bid = x.Bid,
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    Amount = x.Amount,
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (bets == null) return null;

            return bets;
        }

        public IList<BetsDTO> GetBetsFromPlayerAndGame(int page, int pageSize, String playername, int gid)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets
                .Include(x => x.Game)
                .Where(x => x.PlayerName.Equals(playername) && x.Gid.Equals(gid))
                .Select(x => new BetsDTO
                {
                    Bid = x.Bid,
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    Amount = x.Amount,
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (bets == null) return null;

            return bets;
        }

        public IList<BetsDTO> GetBetsFromPlayer(int page, int pageSize, string playername)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets
            .Where(x => x.PlayerName.Equals(playername))
            .Select(x => new BetsDTO
                {
                    Bid = x.Bid,
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    Amount = x.Amount,
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (bets == null) return null;

            return bets;
        }

        public IList<BetsDTO> GetBetsFromGame(int page, int pageSize, int gid)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets
            .Where(x => x.Gid.Equals(gid))
            .Select(x => new BetsDTO
            {
                Bid = x.Bid,
                PlayerName = x.PlayerName,
                Gid = x.Gid,
                Amount = x.Amount,
            })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (bets == null) return null;

            return bets;
        }

        public IList<BetDTO>? GetGameBets(int gid)
        {
            using var db = new CasinoDBContext();

            var bets = db.Bets?
                .Where(x => x.Gid.Equals(gid))
                .Select(x => new BetDTO
                {
                    Bid = x.Bid,
                    PlayerName = x.PlayerName,
                    Gid = x.Gid,
                    Amount = x.Amount,
                })
                .ToList();

            return bets;
        }


        //Helper functions
        public int GetNumberOfBets()
        {
            using var db = new CasinoDBContext();

            return db.Bets
                .Select(x => new BetsDTO { }).Count();
        }
    }
}
