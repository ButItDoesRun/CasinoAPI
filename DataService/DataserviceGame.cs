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

        public SpecificGame CreateGame(string name, float minbet, float maxbet, float? potamount)
        {
            throw new NotImplementedException();
        }

        public SpecificGame CreateGame(string name, float minbet, float maxbet)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGame(int gid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGame(int gid, string name, float minbet, float maxbet)
        {
            throw new NotImplementedException();
        }

        public SpecificMoneyPot AddGamePot(int gid, float amount)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGamePot(int gid, float amount)
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
