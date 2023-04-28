using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class MoneyPot
    {
        public int Pid { get; set; }
        public int Gid { get; set; }
        public float? Amount { get; set; }

        //references
        public Game? Game { get; set; }
    }
}
