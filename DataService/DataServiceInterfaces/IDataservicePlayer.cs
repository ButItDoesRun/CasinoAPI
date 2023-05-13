using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataservicePlayer
    {
        public IList<PlayersDTO>? GetPlayers(int page, int pageSize);
        public int GetNumberOfPlayers();
        public PlayerDTO? GetPlayerByID(String name);
        public bool CreatePlayer(string playername, string password, DateOnly birthdate, double balance, string ssalt);
        public bool PlayerExists(string playername);
        public PlayerDTO? UpdatePlayerBalance(string playername, double amount);
        public double? GetPlayerBalance(String playername);
        public Player? GetPlayerObject(String name);
        public bool DeletePlayer(string playername);
    }
}
