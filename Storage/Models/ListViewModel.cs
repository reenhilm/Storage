using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class ListViewModel
    {
        public IEnumerable<Storage.Models.ProductViewModel>? ProductList { get; set; }
        public IEnumerable<string>? CategoryList { get; set; }

        public string? Category { get; set; }

        [MaxLength(50)]
        public string? ProductName { get; set; } 
    }
}
