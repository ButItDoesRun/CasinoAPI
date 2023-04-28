using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;

namespace DataLayer
{
    public class DataserviceGame : IDataserviceGame
    {
        public SpecificGame GetGameById(int gid)
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

            return game;
        }
    }
}
