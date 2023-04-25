using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataLayer.DatabaseModel;
using DataLayer.DatabaseModel.CasinoModel;
using DataLayer.DataServiceInterfaces;
using DataLayer.DataTransferModel;

namespace DataLayer
{
    public class Dataservice :IDataservice
    {

        public SpecificGame GetGameById(int gid)
        {
            using var db = new CasinoDBContext();
            var game = db.Games
                .Select(x => new SpecificGame
                {
                    Gid = x.Gid,
                    Name = x.Name
                })
                .FirstOrDefault(x => x.Gid == gid);

            return game;
        }

    }
}
