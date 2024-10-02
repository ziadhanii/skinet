namespace Core.Specifications
{
    public class ProductSpecParams
    {

        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        private List<string> _brands = new List<string>();
        public List<string> Brands
        {
            get => _brands;
            set
            {
                _brands = value.Select(x => x.Split(',',
                    StringSplitOptions.RemoveEmptyEntries)).SelectMany(x => x).ToList();
            }
        }

        private List<string> _types = new List<string>();
        public List<string> Types
        {
            get => _types;
            set
            {
                _types = value.Select(x => x.Split(',',
                    StringSplitOptions.RemoveEmptyEntries)).SelectMany(x => x).ToList();
            }
        }

        public string? Sort { get; set; }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }
    }
}
