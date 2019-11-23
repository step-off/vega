namespace vega.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortingDescending { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}