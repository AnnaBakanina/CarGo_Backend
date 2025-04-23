namespace Backend.Extensions;

public interface IQueryObject
{
    string SortBy { get; set; }
    bool IsSortAscending { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}