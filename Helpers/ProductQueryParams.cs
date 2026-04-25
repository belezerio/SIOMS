namespace SIOMS.Helpers;

public class ProductQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string? SortBy { get; set; }
    public string? Order { get; set; } = "asc";
}