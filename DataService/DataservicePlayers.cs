using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataservicePlayers : IDataservicePlayers
    {
        public IList<PlayerListElement> GetPlayers(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var players = db.Players
                .Select(x => new PlayerListElement
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

            return db.Players
                .Select(x => new PlayerListElement { }).Count();
        }
        
    }
}
