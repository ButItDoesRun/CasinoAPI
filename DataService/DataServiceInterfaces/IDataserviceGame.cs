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
        public SpecificGame GetGameById(int gid);

        public SpecificGame CreateGame(string name, float minbet, float maxbet, float? potamount);
        public SpecificGame CreateGame(string name, float minbet, float maxbet);

        public bool DeleteGame(int gid);

        public bool UpdateGame(int gid, string name, float minbet, float maxbet);

        public SpecificMoneyPot AddGamePot(int gid, float amount);

        public bool UpdateGamePot(int gid, float amount);

        public bool DeleteGamePot(int gid);

        public bool DeleteGamePots(int gid);

    }
}
