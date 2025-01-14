namespace NewsFeedApp.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {

        }

        public Pager(int totalItems, int currentPage, int pageSize = 10)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)TotalItems / PageSize);
            CurrentPage = currentPage < 1 ? 1 : currentPage > TotalPages ? TotalPages : currentPage;

            StartPage = CurrentPage - 3;
            EndPage = CurrentPage + 4;

            if (StartPage <= 0)
            {
                EndPage = EndPage - (StartPage - 1);
                StartPage = 1;
            }

            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 10)
                {
                    StartPage = EndPage - 9;
                }
            }
        }
    }
}
