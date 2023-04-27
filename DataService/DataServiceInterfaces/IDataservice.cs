using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataServiceInterfaces
{
    public interface IDataservice
    {
        //public SpecificMoneyPot GetMoneyPotById(int pid);
        // public SpecificMoneyPot GetMoneyPotByGameId(int gid);

        public SpecificPlayer GetPlayerByName(String name);

    }
}
