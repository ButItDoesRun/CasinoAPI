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
        public IList<BetsDTO> GetBets(int page, int pageSize);
        public IList<BetsDTO> GetBetsFromPlayerAndGame(int page, int pageSize, String playername, int gid);
        public int GetNumberOfBets();
    }
}
