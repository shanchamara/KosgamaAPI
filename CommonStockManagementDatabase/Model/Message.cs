namespace CommonStockManagementDatabase.Model
{
    public class Message<TEntity>
    {
        public string Status { get; set; } = "E";
        public string Text { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public TEntity Result { get; set; }
    }

    public class DataResponse<TEntity>
    {
        public TEntity Data { get; set; }
        public Payload Payload { get; set; }
    }

    public class NewDataResponse<TEntity>
    {
        public TEntity Data { get; set; }
    }

    public class Arrylsit
    {
        public string Url { get; set; }
        public string Label { get; set; }
        public bool Active { get; set; }
        public int? Page { get; set; } = null;
    }

    public class PaginationInfo
    {
        public List<Arrylsit> Links { get; set; }
        public int Page { get; set; }
        public string First_page_url { get; set; }
        public int From { get; set; }
        public int Last_page { get; set; }
        public string Next_page_url { get; set; }
        public int Items_per_page { get; set; }
        public string Prev_page_url { get; set; }
        public int To { get; set; }
        public int Total { get; set; }
        public decimal? ToatalAmount { get; set; } = 0.00m;
        public decimal? ToatalDiscount { get; set; } = 0.00m;
        public decimal? ToatalGross { get; set; } = 0.00m;
    }

    public class Payload
    {
        public PaginationInfo Pagination { get; set; }
    }

    public class PaginationHelper<TEntity>(int itemsPerPage, int totalItems, decimal? totalAmount = 0, decimal? discount = 0, decimal? gross = 0)
    {
        public int Items_per_page { get; set; } = itemsPerPage;
        public int TotalItems { get; set; } = totalItems;
        public decimal? ToatalAmount { get; set; } = totalAmount;
        public decimal? ToatalDiscount { get; set; } = discount;
        public decimal? ToatalGross { get; set; } = gross;

        public PaginationInfo GetPaginationInfo(int currentPage)
        {
            double totalPages = (double)TotalItems / Items_per_page;
            int roundedUpPages = (int)Math.Ceiling(totalPages);

            List<Arrylsit> paginationButtons = new();

            // Add Previous button if not on the first page
            if (currentPage > 1)
            {
                paginationButtons.Add(new Arrylsit { Active = false, Label = "&laquo; Previous", Url = $"/?page={currentPage - 1}", Page = currentPage - 1 });
            }

            // Calculate the range of pages to display, limiting to 4 pages
            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(startPage + 3, roundedUpPages);

            // Add page buttons
            for (int i = startPage; i <= endPage; i++)
            {
                paginationButtons.Add(new Arrylsit
                {
                    Page = i,
                    Active = i == currentPage,
                    Label = i.ToString(),
                    Url = $"/?page={i}"
                });
            }

            // Add Next button if not on the last page
            if (currentPage < roundedUpPages)
            {
                paginationButtons.Add(new Arrylsit { Active = false, Label = "Next &raquo;", Url = $"/?page={currentPage + 1}", Page = currentPage + 1 });
            }

            return new PaginationInfo // a custom class for holding pagination information
            {
                First_page_url = $"/?page={1}",
                Page = currentPage,
                From = (currentPage * Items_per_page) - Items_per_page + 1,
                To = (currentPage * Items_per_page),
                Last_page = roundedUpPages,
                Items_per_page = Items_per_page,
                Prev_page_url = currentPage == 1 ? null : $"/?page={currentPage - 1}",
                Next_page_url = currentPage + 1 > roundedUpPages == true ? null : $"/?page={currentPage + 1}",
                Total = TotalItems,
                Links = paginationButtons,
                ToatalAmount = ToatalAmount,
                ToatalGross = ToatalGross,
                ToatalDiscount = ToatalDiscount,
            };


        }
    }
}
