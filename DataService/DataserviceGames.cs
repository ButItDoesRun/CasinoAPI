using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceGames : IDataserviceGames
    {
        /*

        public IList<GameListElement> GetGames(int page, int pageSize)
        {
            using var db = new CasinoDBContext();

            var games = db.Games
                .Select(x => new GameListElement
                {
                    Gid = x.Gid,
                    Name = x.Name
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            if (games == null) return null;

            return games;
        }


        //Helper functions
        public int GetNumberOfGames()
        {
            using var db = new CasinoDBContext();

            return db.Games
                .Select(x => new GameListElement {}).Count();
        }
        */
    }
}
