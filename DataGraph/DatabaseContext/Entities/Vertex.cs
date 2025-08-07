namespace DataGraph.DatabaseContext.Entities;

public class Vertex
{
    public long Id { get; set; }
    public long NodeTypeId { get; set; }
    public string Data { get; set; }
}
