using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class Bet
    {
        public int? Uid { get; set; }
        public int? Gid { get; set; }
        public float? Amount { get; set; }
        public DateTime Date { get; set; }


        //references
        public Game? Game { get; set; }
        public Customer? Customer { get; set; }  

    }
}
