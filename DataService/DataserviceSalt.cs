using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataserviceSalt : IDataserviceSalt
    {
        public SpecificSalt GetSaltByName(string name)
        {
            using var db = new CasinoDBContext();
            var salt = db.Salts
                .Select(x => new SpecificSalt
                {
                    PlayerName = x.PlayerName,
                    SSalt = x.SSalt,
                })
                .FirstOrDefault(x => x.PlayerName == name);

            return salt;
        }
    }
}
