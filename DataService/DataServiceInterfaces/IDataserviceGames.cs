using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataserviceGames
    {

        IList<GameListElement> GetGames(int page, int pageSize);
        public int GetNumberOfGames();
    }
}
