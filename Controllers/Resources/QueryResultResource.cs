namespace Backend.Controllers.Resources;

public class QueryResultResource<T>
{
    public int TotalItems { get; set; }
    public IEnumerable<T> Data { get; set; }
}