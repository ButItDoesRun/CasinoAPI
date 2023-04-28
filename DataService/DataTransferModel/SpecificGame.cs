using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataTransferModel
{
    public class SpecificGame
    {
        public int Gid { get; set; }
        public string? Name { get; set; }
        public float? MinBet { get; set; }
        public float? MaxBet { get; set; }
        public int? Pid { get; set; }
        public float? PotAmount { get; set; }

    }
}
