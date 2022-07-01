namespace Storage.Models
{
    public class ListViewModel : ProductViewModel
    {
        public IEnumerable<Storage.Models.ProductViewModel> ProductList { get; set; }
        public IEnumerable<string> CategoryList { get; set; }
    }
}
