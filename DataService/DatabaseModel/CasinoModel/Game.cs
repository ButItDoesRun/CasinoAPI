using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class Game
    {
        public int? Gid { get; set; }
        public string? Name { get; set; }
        public double? MinBet { get; set; }
        public double? MaxBet { get; set; }
        public int? Pid { get; set; }

        //references
        public MoneyPot? MoneyPot { get; set; }
        public IList<Bet>? Bet { get; set; }

    }
}
