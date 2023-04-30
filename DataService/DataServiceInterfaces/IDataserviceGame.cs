using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataserviceGame
    {
        public GameDTO? GetGameById(int gid);

        public GameDTO? CreateGame(string name, double minbet, double maxbet, double? potamount);
        public GameDTO? CreateGame(string name, double minbet, double maxbet);

        public bool DeleteGame(int gid);

        public GameDTO? UpdateGame(int gid, string name, double minbet, double maxbet);

        public GameDTO? AddGamePot(int gid, double amount);

        public GameDTO? UpdateGamePot(int gid, double amount);

        public bool DeleteGamePot(int gid);

        public bool DeleteGamePots(int gid);

    }
}
