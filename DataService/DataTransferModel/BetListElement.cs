using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataTransferModel
{
    public class BetListElement
    {
        public string? PlayerName { get; set; }
        public int? Gid { get; set; }
        public string? GameName { get; set; }
        public double? Amount { get; set; }
    }
}
