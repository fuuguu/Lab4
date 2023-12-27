using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Lab3
{
    public class Renter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set;}
        [Required]
        public int NumOfResidents {  get; set; } //число проживающих
        [Required]
        public double LivingSpace { get; set; } //жилая площадь
        [Required]
        public string? ApartType { get; set; } //тип квартиры
        [Required]
        public double CostOfLiving {  get; set; } //стоимость квартиры
        [Required]
        public double RentAmount { get; set; } //сумма квартплаты
        public int PriceListId {  get; set; }
        public PriceList? PriceList { get; set; }
    }
}
