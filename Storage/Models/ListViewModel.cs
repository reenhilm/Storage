namespace Storage.Models
{
    public class ListViewModel
    {
        public IEnumerable<Storage.Models.ProductViewModel> ProductList { get; set; }
        public IEnumerable<string> CategoryList { get; set; }
    }
}
