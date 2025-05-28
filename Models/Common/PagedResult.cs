namespace TFG_BACK.Models.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }

    public class PaginationRequest
    {
        private int _page = 1;
        private int _pageSize = 10;

        public int Page 
        { 
            get => _page; 
            set => _page = value <= 0 ? 1 : value; 
        }
        
        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = value <= 0 ? 10 : value > 100 ? 100 : value; 
        }
    }
}