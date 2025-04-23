namespace Backend.Models;

public class QueryResult<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; }
}