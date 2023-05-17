using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class Bet
    {
        public int Bid { get; set; }
        public string? PlayerName { get; set; }
        public int Gid { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }


        //references
        public Game? Game { get; set; }
        public Player? Player { get; set; }  

    }
}
