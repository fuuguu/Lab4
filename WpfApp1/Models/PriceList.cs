using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class PriceList
    {
        public int Id { get; set; }
        public string? ApartType { get; set; }
        public double PricePerMeter { get; set; }
        public double Utilities { get; set; }
        List<Renter> Renters { get; set; } = new();
    }
}
