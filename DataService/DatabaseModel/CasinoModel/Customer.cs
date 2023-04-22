using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseModel.CasinoModel
{
    public class Customer
    {
        public int? Uid { get; set; }
        public string? Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Password { get; set; }
        public float? Balance { get; set; }   
        
        //references
        public IList<Bet>? Bet { get; set; }
        public Salt? Salt { get; set; }
    }
}
