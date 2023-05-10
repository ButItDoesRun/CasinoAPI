using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataTransferModel
{
    public class PlayerDTO
    {
        public string? PlayerName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Password { get; set; }
        public double? Balance { get; set; }

    }
}
