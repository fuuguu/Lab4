using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Renter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int NumOfResidents { get; set; } //число проживающих
        public double LivingSpace { get; set; } //жилая площадь
        public string? ApartType { get; set; } //тип квартиры
        public double CostOfLiving { get; set; } //стоимость квартиры
        public double RentAmount { get; set; } //сумма квартплаты
        public int PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
    }
}
