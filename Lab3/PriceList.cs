using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lab3
{
    public class PriceList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Key]
        public string? ApartType {  get; set; }
        [Required]
        public double PricePerMeter { get; set; }
        [Required]
        public double Utilities { get; set; }
        [Required]
        List<Renter> Renters { get; set; } = new();
    }
}
