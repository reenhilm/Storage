using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    #nullable disable
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Orderdate { get; set; }
        
        [Required]
        public string Category { get; set; }

        [MaxLength(50)]
        public string Shelf { get; set; }
        
        [Range(0, 10)]
        public int Count { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
    }
}
