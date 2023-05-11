using DataLayer.DatabaseModel.CasinoModel;
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

    }
}
