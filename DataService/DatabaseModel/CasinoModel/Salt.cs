using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class Salt
    {
        public int Uid { get; set; }
        public string SSalt { get; set; }

        //references
        public Customer Customer { get; set; }
    }
}
