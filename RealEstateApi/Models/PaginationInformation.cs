namespace RealEstateApi.Models
{
    public class PaginationInformation
    {
        public int PageNumber { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public bool HasNext => PageNumber < TotalPages;

        public bool HasPrevious => PageNumber > 1;


    }
}
