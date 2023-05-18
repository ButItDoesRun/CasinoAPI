using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataserviceBets
    {
        //All bets
        public IList<BetsDTO> GetBets();
        public IList<BetsDTO> GetBetsWithPaging(int page, int pageSize);

        //Bets from the same player and game
        public IList<BetsDTO> GetBetsFromPlayerAndGame(String playername, int gid);
        public IList<BetsDTO> GetBetsFromPlayerAndGameWithPaging(int page, int pageSize, String playername, int gid);

        //Bets from a specific player
        public IList<BetsDTO> GetBetsFromPlayer(string playername);
        public IList<BetsDTO> GetBetsFromPlayerWithPaging(int page, int pageSize, string playername);

        //Bets from a specific game
        public IList<BetsDTO> GetBetsFromGame(int gid);
        public IList<BetsDTO> GetBetsFromGameWithPaging(int page, int pageSize, int gid);
        public IList<BetDTO>? GetGameBets(int gid);
        public IList<BetDTO> GetPlayerBets(String playername, int gid);
        public int GetNumberOfBets();
    }
}
